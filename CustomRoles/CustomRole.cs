using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features.Attributes;
using MEC;
using UnityEngine;
using VoiceChat;

namespace JacobsToolbox.CustomRoles
{
    [CustomRole(RoleTypeId.Tutorial)]
    public class KomarRole : CustomRole
    {
        // Static instance for global access
        public static KomarRole Instance { get; private set; }

        // Static constructor to initialize the instance
        static KomarRole()
        {
            Instance = new KomarRole();
        }

        public override string Name { get; set; } = "SCP-KOMAR";
        public override string Description { get; set; } = "Maly Wkurwiajacy komarek";
        public override string CustomInfo { get; set; } = "SCP-KOMAR";
        public override uint Id { get; set; } = 2137;
        public override int MaxHealth { get; set; } = 1;

        public override bool KeepPositionOnSpawn { get; set; } = true;
        public override bool KeepInventoryOnSpawn { get; set; } = false;
        public override bool RemovalKillsPlayer { get; set; } = true;
        public override bool KeepRoleOnDeath { get; set; } = false;
        public override bool IgnoreSpawnSystem { get; set; } = true;
        public override bool KeepRoleOnChangingRole { get; set; } = false;
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        
        
        public override void AddRole(Player player)
        {
            // Setup of a custom role
            base.AddRole(player);
            player.EnableEffect<Disabled>();
            player.EnableEffect<SilentWalk>();
            player.ChangeEffectIntensity<SilentWalk>(10);
            player.IsMuted = false;
            Vector3 size = Vector3.one * 0.1f;
            player.SessionVariables.Add("IsKomar", true);
            // player.SessionVariables.Add("SVC", true);
            player.IsNoclipPermitted = true;
            player.IsUsingStamina = false;
            player.Handcuff();

            Timing.CallDelayed(0.1f, () =>
            {
                player.EnableEffect<Ghostly>();
                player.Broadcast(10, "<color=green>Możesz przełączyć czat głosowy obserwatorów używając komendy</color> <color=yellow>.svc</color>");
                player.Scale = size;
            });
        }
        
        public override void RemoveRole(Player player)
        {
            // Remove a custom role
            base.RemoveRole(player);
            player.SetScale(new Vector3(1f, 1f, 1f), Player.List);
            player.IsNoclipPermitted = false;
            player.IsMuted = false;
            player.IsUsingStamina = true;
            player.RemoveHandcuffs();
            player.SessionVariables.Remove("IsKomar");
            player.SessionVariables.Remove("SVC");
        }
    }
}