using System;
using System.Diagnostics.Eventing.Reader;
using System.Security;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using JacobsToolbox.CustomRoles;
using NetworkManagerUtils.Dummies;
using PlayerRoles;
using Map = Exiled.API.Features.Map;
using Round = LabApi.Features.Wrappers.Round;
using Exiled.Events.EventArgs.Scp914;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.Patches.Events.Player;
using Exiled.Permissions.Commands.Permissions.Group;
using LabApi.Events.Arguments.ServerEvents;
using MEC;
using PlayerRoles.PlayableScps.Subroutines;
using Scp914;
using UnityEngine;
using UserSettings.ServerSpecific;
using Group = Exiled.Permissions.Features.Group;
using Player = Exiled.Events.Handlers.Player;

namespace JacobsToolbox.EventHandlers
{
    public static class General
    {
        private static bool IsFirstJoin { get; set; } = true;
        public static void OnVerified(VerifiedEventArgs ev)
        {
            if (Plugin.Instance.Config.Debug)
            {
                Log.Debug("Sent Broadcast to " + ev.Player.Nickname);
                ev.Player.Broadcast(5, "Plugin initialized!");
                if (IsFirstJoin)
                {
                    Log.Debug("First join detected, initializing QuickStart.");
                    Round.IsLobbyLocked = true;
                    Round.IsLocked = true;
                    Map.IsDecontaminationEnabled = false;
                    DummyUtils.SpawnDummy("TestDummy");
                    IsFirstJoin = false;
                }
            }
            else
            {
                ev.Player.Broadcast(5, "Witaj na serwerze! Aktualnie mamy 30 efektów monetki \nMożesz je sugerować w wątku na #scp-sugesje na discordzie \nZajrzyj też na #scp-info");
            }
        }
        public static void OnDeath(DiedEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsKomar"))
            {
                Log.Debug("Player died, removing Komar role.");
                KomarRole.Instance.RemoveRole(ev.Player);
            }
        }
        
        public static void OnLeft(LeftEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsKomar"))
            {
                Log.Debug("Player leving, removing Komar role.");
                KomarRole.Instance.RemoveRole(ev.Player);
            }
        }

        public static void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("OldMansTouch"))
            {
                CoinFlipEffects.OldMansTouch.Remove(ev.Player);
            }
            if (ev.Player.SessionVariables.ContainsKey("Vampire"))
            {
                ev.Player.SessionVariables.Remove("Vampire");
            }
            if (ev.Player.SessionVariables.ContainsKey("MaxHp"))
            {
                ev.Player.SessionVariables.Remove("MaxHp");
            }
            if (ev.Player.SessionVariables.ContainsKey("IsKomar"))
            {
                Log.Debug("Changing role, removing Komar role.");
                KomarRole.Instance.RemoveRole(ev.Player);
            }

            if (ev.Player.SessionVariables.ContainsKey("Vanish"))
            {
                ev.Player.ChangeAppearance(RoleTypeId.Spectator);
            }

            if (Plugin.Instance.Config.RemoveUnits)
            {
                ev.Player.InfoArea &= ~PlayerInfoArea.UnitName;
            }
        }

        public static void OnUpgradingPickup(UpgradingPickupEventArgs ev)
        {
            if (ev.Pickup.Type == ItemType.SCP2176 && ev.KnobSetting == Scp914KnobSetting.OneToOne && Plugin.Instance.Config.Prevent2176Upgrade)
            {
                Log.Debug("SCP-2176 being upgraded on 1:1, preventing upgrade.");
                ev.IsAllowed = false;
            }
        }

        public static void OnUpgradingInventoryItem(UpgradingInventoryItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.SCP2176 && ev.KnobSetting == Scp914KnobSetting.OneToOne && Plugin.Instance.Config.Prevent2176Upgrade)
            {
                Log.Debug("SCP-2176 being upgraded on 1:1, preventing upgrade.");
                ev.IsAllowed = false;
            }
        }

        public static void OnFlippingCoin(FlippingCoinEventArgs ev)
        {
            if (Plugin.Instance.Config.EnableMysteryCoins)
            {
                Timing.CallDelayed(2f, () =>
                {
                    if (ev.IsTails) ev.Player.CurrentItem.Destroy();
                    CoinFlipEffect.CoinEffect(ev.Player);
                
                    if (ev.IsTails)
                    {
                        Log.Debug("Player flipped a coin and got tails.");
                        Hint hint = new Hint()
                        {
                            Content = "Wyrzuciłeś orła!",
                            Duration = 3f,
                        };
                        ev.Player.ShowHint(hint);
                    }
                    else
                    {
                        Log.Debug("Player flipped a coin and got heads.");
                        Hint hint = new Hint()
                        {
                            Content = "Wyrzuciłeś reszkę!",
                            Duration = 3f,
                        };
                        ev.Player.ShowHint(hint);
                    }
                });
            }
        }

        public static void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null) return;
            if (ev.Attacker.SessionVariables.ContainsKey("Vampire"))
            {
                var damage = ev.DamageHandler.Damage*0.1f;
                LabApi.Features.Wrappers.Player lapiPly = LabApi.Features.Wrappers.Player.Get(ev.Attacker.Id);
                ev.Attacker.SessionVariables.TryGetValue("MaxHp", out var  maxHp);
                if (lapiPly.Health + damage > (float)maxHp * 1.5f)
                {
                    lapiPly.Health = (float) maxHp * 1.5f;
                    return;
                }
                lapiPly.Health += damage;
            }
        }

        // public static void OnSpawningItem(SpawningItemEventArgs ev)
        // {
        //     if (ev.Pickup.Type == ItemType.Coin)
        //     {
        //         ev.ShouldInitiallySpawn = false;
        //         ev.IsAllowed = false;
        //     }
        // }
        //
        // public static void OnFillingLocker(FillingLockerEventArgs ev)
        // {
        //     if (ev.Pickup.Type == ItemType.Coin)
        //     {
        //         ev.IsAllowed = false;
        //     }
        //     
        // }
    }
}