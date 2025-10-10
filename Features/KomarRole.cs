using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Scp173;
using Exiled.Events.EventArgs.Scp939;
using Exiled.Events.EventArgs.Server;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Utilities;
using PlayerRoles;
using UnityEngine;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;
using PlayerEventHandler = Exiled.Events.Handlers.Player;
using Scp173EventHandler = Exiled.Events.Handlers.Scp173;
using Scp096EventHandler = Exiled.Events.Handlers.Scp096;
using Scp939EventHandler = Exiled.Events.Handlers.Scp939;
using ServerEventHandler = Exiled.Events.Handlers.Server;

namespace JacobsToolbox.Features;

[CustomRole(RoleTypeId.Tutorial)]
public class KomarRole : CustomRole
{ 
    public override string CustomInfo { get; set; } = "Komar"; 
    public override string Name { get; set; } = "Komar"; 
    public override string Description { get; set; } = "Flying little piece of shit."; 
    public override RoleTypeId Role => RoleTypeId.Tutorial; 
    public override uint Id { get; set; } = 2137; 
    public override int MaxHealth { get; set; } = 1;
    public override bool IgnoreSpawnSystem { get; set; } = true;
    public override bool KeepRoleOnChangingRole { get; set; } = false;
    public override bool KeepRoleOnDeath { get; set; } = false;
    public override bool KeepPositionOnSpawn { get; set; } = false;
    public override Vector3 Scale { get; set; } = new (0.1f, 0.1f, 0.1f);

    public override List<string> Inventory { get; set; } = new()
    {
        $"{ItemType.Coin}",
        $"{ItemType.SurfaceAccessPass}",
        $"{ItemType.KeycardGuard}",
        $"{ItemType.KeycardContainmentEngineer}",
        $"{ItemType.KeycardScientist}"
    };

    private Hint tip = new Hint()
    {
        Text = "TIP: Wyrzuć przedmiot aby się teleportować\nOdrodzisz sie normalnie przy respie.",
        Alignment = HintAlignment.Right,
        YCoordinateAlign = HintVerticalAlign.Top,
        FontSize = 20,
        YCoordinate = 5
    };

    public override void AddRole(Player player)
    {
        PlayerDisplay playerDisplay = PlayerDisplay.Get(player);
        playerDisplay.AddHint(tip);
        player.IsNoclipPermitted = true;
        player.CustomName = player.Nickname + " (Komar)";
        base.AddRole(player);
    }

    public override void RemoveRole(Player player)
    {
        PlayerDisplay playerDisplay = PlayerDisplay.Get(player);
        playerDisplay.RemoveHint(tip);
        player.IsNoclipPermitted = false;
        player.CustomName = null;
        base.RemoveRole(player);
    }

    protected override void SubscribeEvents()
    {
        PlayerEventHandler.InteractingDoor += OnInteractingDoor;
        PlayerEventHandler.InteractingElevator += OnInteractingElevator;
        PlayerEventHandler.UsingItem += OnUsingItem;
        PlayerEventHandler.PickingUpItem += OnPickingUpItem;
        PlayerEventHandler.TriggeringTesla += OnTriggeringTesla;
        PlayerEventHandler.ActivatingWarheadPanel += OnActivatingWarheadPanel;
        PlayerEventHandler.ActivatingGenerator += OnActivatingGenerator;
        PlayerEventHandler.ActivatingWorkstation += OnActivatingWorkstation;
        PlayerEventHandler.StoppingGenerator += OnStoppingGenerator;
        PlayerEventHandler.OpeningGenerator += OnOpeningGenerator;
        PlayerEventHandler.ClosingGenerator += OnClosingGenerator;
        PlayerEventHandler.DeactivatingWorkstation += OnDeactivatingWorkstation;
        PlayerEventHandler.InteractingLocker += OnInteractingLocker;
        PlayerEventHandler.Handcuffing += OnHandcuffing;
        PlayerEventHandler.RemovingHandcuffs += OnRemovingHandcuffs;
        PlayerEventHandler.DroppingItem += OnDroppingItem;
        PlayerEventHandler.DroppingAmmo += OnDroppingAmmo;
        PlayerEventHandler.TogglingRadio += OnTogglingRadio;
        PlayerEventHandler.IntercomSpeaking += OnIntercomSpeaking;
        Scp173EventHandler.AddingObserver += OnAddingObserver;
        Scp096EventHandler.AddingTarget += OnAddingTarget;
        Scp939EventHandler.ValidatingVisibility += OnValidatingVisibility;
        Scp939EventHandler.PlayingFootstep += OnPlayingFootstep;
        PlayerEventHandler.SpawningRagdoll += OnSpawningRagdoll;
        PlayerEventHandler.ThrowingRequest += OnThrowingRequest;
        PlayerEventHandler.ChangingRole += OnChangingRole;
        PlayerEventHandler.Dying += OnDying;
        PlayerEventHandler.Left += OnPlayerLeft;
        ServerEventHandler.RespawningTeam += OnRespawningTeam;
        
        
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        PlayerEventHandler.InteractingDoor -= OnInteractingDoor;
        PlayerEventHandler.InteractingElevator -= OnInteractingElevator;
        PlayerEventHandler.UsingItem -= OnUsingItem;
        PlayerEventHandler.PickingUpItem -= OnPickingUpItem;
        PlayerEventHandler.TriggeringTesla -= OnTriggeringTesla;
        PlayerEventHandler.ActivatingWarheadPanel -= OnActivatingWarheadPanel;
        PlayerEventHandler.ActivatingGenerator -= OnActivatingGenerator;
        PlayerEventHandler.ActivatingWorkstation -= OnActivatingWorkstation;
        PlayerEventHandler.StoppingGenerator -= OnStoppingGenerator;
        PlayerEventHandler.OpeningGenerator -= OnOpeningGenerator;
        PlayerEventHandler.ClosingGenerator -= OnClosingGenerator;
        PlayerEventHandler.DeactivatingWorkstation -= OnDeactivatingWorkstation;
        PlayerEventHandler.InteractingLocker -= OnInteractingLocker;
        PlayerEventHandler.Handcuffing -= OnHandcuffing;
        PlayerEventHandler.RemovingHandcuffs -= OnRemovingHandcuffs;
        PlayerEventHandler.DroppingItem -= OnDroppingItem;
        PlayerEventHandler.DroppingAmmo -= OnDroppingAmmo;
        PlayerEventHandler.TogglingRadio -= OnTogglingRadio;
        PlayerEventHandler.IntercomSpeaking -= OnIntercomSpeaking;
        Scp173EventHandler.AddingObserver -= OnAddingObserver;
        Scp096EventHandler.AddingTarget -= OnAddingTarget;
        Scp939EventHandler.ValidatingVisibility -= OnValidatingVisibility;
        Scp939EventHandler.PlayingFootstep -= OnPlayingFootstep;
        PlayerEventHandler.SpawningRagdoll -= OnSpawningRagdoll;
        PlayerEventHandler.ThrowingRequest -= OnThrowingRequest;
        PlayerEventHandler.ChangingRole -= OnChangingRole;
        PlayerEventHandler.Dying -= OnDying;
        PlayerEventHandler.Left -= OnPlayerLeft;
        ServerEventHandler.RespawningTeam -= OnRespawningTeam;
        
        base.UnsubscribeEvents();
    }

    private void OnInteractingDoor(InteractingDoorEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false; 
    }

    private void OnInteractingElevator(InteractingElevatorEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnUsingItem(UsingItemEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnPickingUpItem(PickingUpItemEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }
    
    private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnActivatingWorkstation(ActivatingWorkstationEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnStoppingGenerator(StoppingGeneratorEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnOpeningGenerator(OpeningGeneratorEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnClosingGenerator(ClosingGeneratorEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnDeactivatingWorkstation(DeactivatingWorkstationEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnInteractingLocker(InteractingLockerEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnHandcuffing(HandcuffingEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnDroppingItem(DroppingItemEventArgs ev)
    {
        if (Check(ev.Player))
        {
            ev.IsAllowed = false;
            switch (ev.Item.Type)
            {
                case ItemType.Coin:
                {
                    // Pick a random alive player that is not the actor
                    var candidates = new List<Player>();
                    foreach (var p in Player.List)
                    {
                        if (p != null && p != ev.Player && p.IsAlive)
                            candidates.Add(p);
                    }

                    if (candidates.Count == 0)
                    {
                        ev.Player.Broadcast(3, "Brak żywych celów do teleportacji.");
                        break;
                    }

                    var index = UnityEngine.Random.Range(0, candidates.Count);
                    var target = candidates[index];

                    // Teleport the actor to the target's position
                    ev.Player.Position = target.Position;
                    break;
                }
                case ItemType.KeycardContainmentEngineer:
                    if (Warhead.IsDetonated)
                    {
                        ev.Player.Broadcast(3, "Placowka jest zniszczona, nie możesz się tam teleportować.");
                        break;
                    }

                    ev.Player.Position = Room.Get(RoomType.Hcz096).Position + new Vector3(0, 1, 0);
                    break;
                case ItemType.KeycardScientist:
                    if (Warhead.IsDetonated || Map.IsLczDecontaminated)
                    {
                        ev.Player.Broadcast(3,
                            "Light jest zdekontaminowany/wysadzony, nie możesz się tam teleportować.");
                        break;
                    }

                    ev.Player.Position = Room.Get(RoomType.LczArmory).Position + new Vector3(0, 1, 0);
                    break;
                case ItemType.KeycardGuard:
                    if (Warhead.IsDetonated)
                    {
                        ev.Player.Broadcast(3, "Placowka jest zniszczona, nie możesz się tam teleportować.");
                        break;
                    }

                    ev.Player.Position = Room.Get(RoomType.EzIntercom).Position + new Vector3(0, 1, 0);
                    break;
                case ItemType.SurfaceAccessPass:
                    ev.Player.Position = Room.Get(RoomType.Surface).Position + new Vector3(0, 1, 0);
                    break;

            }
        }
    }
    
    private void OnChangingRole(ChangingRoleEventArgs ev)
    {
        if (Check(ev.Player))
        {
            ev.Player.ClearItems(true);
        }

    }

    private void OnDying(DyingEventArgs ev)
    {
        if (Check(ev.Player))
        {
            ev.Player.ClearItems(true);
        }
    }

    private void OnPlayerLeft(LeftEventArgs ev)
    {
        if (Check(ev.Player))   
        {
            ev.Player.ClearItems();
        }
    }  

    private void OnDroppingAmmo(DroppingAmmoEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnTogglingRadio(TogglingRadioEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    private void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }
    
    private void OnAddingObserver(AddingObserverEventArgs ev)
    {
        if (Check(ev.Observer))
            ev.IsAllowed = false;
    }
    
    private void OnAddingTarget(AddingTargetEventArgs ev)
    {
        if (Check(ev.Target))
            ev.IsAllowed = false;
    }
    
    private void OnValidatingVisibility(ValidatingVisibilityEventArgs ev)
    {
        if (Check(ev.Target))
            ev.IsAllowed = false;
    }

    private void OnPlayingFootstep(PlayingFootstepEventArgs ev)
    {
        if (Check(ev.Target))
            ev.IsAllowed = false;
    }
    
    private void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
    {
        if (Check(ev.Player))
            ev.IsAllowed = false;
    }

    public void OnThrowingRequest(ThrowingRequestEventArgs ev)
    {
        if (Check(ev.Player))
        {
            ev.Player.CurrentItem = null;
            ev.RequestType = ThrowRequest.CancelThrow;
        }
    }

    public void OnRespawningTeam(RespawningTeamEventArgs ev)
    {
        ev.Players.AddRange(Player.Get(API.IsKomar));
    }
}

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Spawn : ICommand
{
    public string Command { get; set; } = "spawnkomar";
    public string Description { get; } = "Rel";
    public string[] Aliases { get; } = new[] { "komar" };
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        var player = Player.Get(sender);
        if (API.IsKomar(player))
        {
            player.Role.Set(RoleTypeId.Spectator);
            response = "Usunięto rolę Komara.";
            return true;
        } else if (player.IsDead)
        {
            response = "Zmieniasz sie w komara.";
            Plugin.Instance.Config.KomarRole.AddRole(player);
            return true;
        }
        else
        {
            response = "Musisz być martwy aby użyć tej komendy.";
            return false;
        }
    }
}