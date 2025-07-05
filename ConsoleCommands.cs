using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.Permissions.Extensions;
using InventorySystem.Items.Pickups;
using JacobsToolbox.CustomRoles;
using static JacobsToolbox.CoinFlipEffect;
using PlayerRoles;
using UnityEngine;

namespace JacobsToolbox
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Explode : ICommand
    {
        public string Command => "explode";
        public string Description => "you explode";
        public string[] Aliases => new[] { "ex", "boom", "EXPLOSION" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if (!ply.CheckPermission("explode.self"))
            {
                response = "You don't have permissions to explode yourself Missing permission: explode.self";
                return false;
            }
            if (ply.IsDead) { response = "you can't explode yourself while being dead."; return false; }
            ply.Explode();
            ply.Kill("You Exploded");
            response = "explode";
            return true;
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Komar : ICommand 
    {
        public string Command => "komar";
        public string Description => "Zmienia cie w komara";
        public string[] Aliases => new[] { "k", "komarrole" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                Player ply = Player.Get(sender);
                if (ply.Role == RoleTypeId.Spectator && ply.Role.ActiveTime.Seconds <= Plugin.Instance.Config.KomarRoleCooldown)
                {
                    response = "Musisz odczekac " + Plugin.Instance.Config.KomarRoleCooldown + " sekund po śmierci aby zmienic sie w komara.";
                    return false;
                }
                if (ply.SessionVariables.ContainsKey("IsKomar"))
                {
                    response = "Zmieniam w obserwatora...";
                    KomarRole.Instance.RemoveRole(ply);
                    return true;
                }
                if (ply.Role != RoleTypeId.Spectator) { response = "Tylko martwi moga stac sie komarem"; return false; }
                if (!sender.CheckPermission("komarrole.self"))
                {
                    response = "Denied. Missing Permission: komarrole.self";
                    return false;
                }
                response = "Zmieniam gracza w komara";
                KomarRole.Instance.AddRole(ply);
                return true;
            }
            else
            {
                if (!sender.CheckPermission("komarrole.others"))
                {
                    response = "Denied. Missing Permission: komarrole.others";
                    return false;
                }

                Player ply = Player.Get(arguments.At(0));
                if (ply == null || ply.AuthenticationType == AuthenticationType.DedicatedServer)
                {
                    response = "Nie znaleziono gracza o podanym nicku";
                    return false;
                }
                if (ply.SessionVariables.ContainsKey("IsKomar"))
                {
                    response = "Zmieniam w obserwatora...";
                    KomarRole.Instance.RemoveRole(ply);
                    return true;
                }
                if (arguments.Count == 1 && ply.Role != RoleTypeId.Spectator && (arguments.At(1) != "-f") )
                {
                    response = "Tylko martwi moga stac sie komarem";
                    return false;
                }
                response = "Zmieniam gracza w komara";
                KomarRole.Instance.AddRole(ply);
                return true;
            }
        }
    }
    
    
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class NoInteraction : ICommand
    {
        public string Command => "nointeraction";
        public string Description => "Disables interaction with the player";
        public string[] Aliases => new[] { "ni", "noint" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                if (!sender.CheckPermission("nointeraction.self"))
                {
                    response = "Denied. Missing Permission: nointeraction.self";
                    return false;
                }
                Player ply = Player.Get(sender);
                if (!ply.SessionVariables.ContainsKey("NoInteraction"))
                {
                    ply.SessionVariables.Add("NoInteraction", true);
                    response = "NoInteraction Enabled";
                }
                else
                {
                    ply.SessionVariables.Remove("NoInteraction");
                    response = "NoInteractionDisabled";
                }
                return true;
            }
            if (!sender.CheckPermission("nointeraction.others"))
            {
                response = "Denied. Missing Permission: nointeraction.others";
                return false;
            }
            Player targetPlayer = Player.Get(arguments.At(0));
            if (targetPlayer == null || targetPlayer.AuthenticationType == AuthenticationType.DedicatedServer)
            {
                response = "Player not found.";
                return false;
            }
            if (!targetPlayer.SessionVariables.ContainsKey("NoInteraction"))
            {
                targetPlayer.SessionVariables.Add("NoInteraction", true);
                response = "NoInteraction Enabled";
            }
            else
            {
                targetPlayer.SessionVariables.Remove("NoInteraction");
                response = "NoInteractionDisabled";
            }
            return true;
        }
    }

    [CommandHandler(typeof(ClientCommandHandler))]
    public class Vanish : ICommand
    {
        public string Command => "vanish";
        public string Description => "hides you from other players (and enables NoInteraction for you)";
        public string[] Aliases => new[] { "v" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                if (!sender.CheckPermission("vanish.self"))
                {
                    response = "Denied. Missing Permission: vanish.self";
                    return false;
                }
                Player ply = Player.Get(sender);
                if (ply.SessionVariables.ContainsKey("Vanish"))
                {
                    ply.ChangeAppearance(ply.Role);
                    ply.SessionVariables.Remove("Vanish");
                    ply.SessionVariables.Remove("NoInteraction");
                    ply.Broadcast(5, "<color=red>You are now visible to other players.</color>");
                    response = "Removed Vanish";
                    return true;
                }
                ply.ChangeAppearance(RoleTypeId.Spectator);
                ply.SessionVariables["NoInteraction"] = true;
                ply.SessionVariables["Vanish"] = true;
                ply.Broadcast(5, "<color=green>You are now invisible to other players.</color>");
                response = "Applied Vanish";
                return true;
            }
            else if (!sender.CheckPermission("vanish.others"))
            {
                response = "Denied. Missing Permission: vanish.others";
                return false;
            }
            Player targetPlayer = Player.Get(arguments.At(0));
            if (targetPlayer == null || targetPlayer.AuthenticationType == AuthenticationType.DedicatedServer)
            {
                response = "Player not found.";
                return false;
            }
            if (targetPlayer.SessionVariables.ContainsKey("Vanish"))
            {
                targetPlayer.ChangeAppearance(targetPlayer.Role);
                targetPlayer.SessionVariables.Remove("Vanish");
                targetPlayer.SessionVariables.Remove("NoInteraction");
                targetPlayer.Broadcast(5, "<color=red>You are now visible to other players.</color>");
                response = "Vanish removed from " + targetPlayer.Nickname;
                return true;
            }
            else
            {
                targetPlayer.ChangeAppearance(RoleTypeId.Spectator, true);
                targetPlayer.SessionVariables["NoInteraction"] = true;
                targetPlayer.SessionVariables["Vanish"] = true;
                targetPlayer.Broadcast(5, "<color=green>You are now invisible to other players.</color>");
                response = "Vanish applied to " + targetPlayer.Nickname;
                return true;
            }
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    public class SpectatorVoiceChat : ICommand
    {
        public string Command => "spectatorvoicechat";
        public string Description => "Przełącza voice chat spectatorów dla komarów";
        public string[] Aliases => new[] { "svc" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if (!API.IsKomar(ply))
            {
                response = "Tylko komary moga uzywac tej komendy.";
                return false;
            }
            if (ply.SessionVariables.ContainsKey("SVC"))
            {
                ply.SessionVariables.Remove("SVC");
                response = "Spectator voice chat disabled.";
            }
            else
            {
                ply.SessionVariables.Add("SVC", true);
                response = "Spectator voice chat enabled.";
            }
            return true;
        }
    }
    
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SessionVariables : ICommand
    {
        public string Command => "sessionvariables";
        public string Description => "Displays all session variables of a player";
        public string[] Aliases => new[] { "sv" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                response = "Usage: sessionvariables <player> (add/delete) [variable]";
                return false;
            }
            

            Player targetPlayer = Player.Get(arguments.At(0));
            if (targetPlayer == null || targetPlayer.AuthenticationType == AuthenticationType.DedicatedServer)
            {
                response = "Player not found.";
                return false;
            }

            if (arguments.Count > 1)
            {
                if (arguments.Count == 2)
                {
                    response = "Usage: sessionvariables <player> (add/delete) [variable]";
                    return false;
                }

                if (arguments.Count == 3)
                {
                    if (arguments.At(1).ToLower() == "add")
                    {
                        targetPlayer.SessionVariables.Add(arguments.At(2), true);
                    }
                    else if (arguments.At(1).ToLower() == "delete")
                    {
                        if (targetPlayer.SessionVariables.ContainsKey(arguments.At(2)))
                        {
                            targetPlayer.SessionVariables.Remove(arguments.At(2));
                        }
                        else
                        {
                            response = $"Variable '{arguments.At(2)}' does not exist for {targetPlayer.Nickname}.";
                            return false;
                        }
                    }
                    else
                    {
                        response = "Invalid action. Use 'add' or 'delete'.";
                        return false;
                    }
                }
            }

            response = $"Session Variables for {targetPlayer.Nickname}:\n";
            foreach (var variable in targetPlayer.SessionVariables)
            {
                response += $"{variable.Key}: {variable.Value}\n";
            }
            return true;
        }
    }

    [CommandHandler(typeof(ClientCommandHandler))]
    public class CoinsLeft : ICommand
    {
        public string Command => "CoinsLeft";
        public string Description => "Pokazuje ile monet zostało w grze";
        public string[] Aliases => new[] { "cl" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var pickups = UnityEngine.Object.FindObjectsByType<ItemPickupBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            int coinCount = 0;
            foreach (var pickup in pickups)
            {
                Pickup pck = Pickup.Get(pickup);
                if (pck.Info.ItemId == ItemType.Coin)
                {
                    coinCount++;
                    Log.Debug("Pickup found: " + pck.Position);
                }
            }

            coinCount--;
            response = "Coins left in the game: " + coinCount;
            return true;
        }
    }

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GiveCoinEffect : ICommand
    {
        public string Command => "givecoineffect";
        public string Description => "Gives a random coin effect to a player";
        public string[] Aliases => new[] { "ce" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                response = "Usage: givecoineffect <player> [effect]";
                return false;
            }

            Player targetPlayer = Player.Get(arguments.At(0));
            if (targetPlayer == null || targetPlayer.AuthenticationType == AuthenticationType.DedicatedServer)
            {
                response = "Player not found.";
                return false;
            }

            CoinEffect(targetPlayer);
            response = $"Coin effect given to {targetPlayer.Nickname}.";
            return true;
        }
    }
}
