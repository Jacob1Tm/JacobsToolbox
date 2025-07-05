using System.Collections.Generic;
using Exiled.API.Features;
using PlayerRoles;

namespace JacobsToolbox.CoinFlipEffects;

public class RoleSwapEffect
{
    public static void Apply(Player player)
    {
        if (player.IsScp)
        {
            player.Broadcast(5, "Trafił*ś... Nic.");
            return;
        }
        player.DropItems();
        player.Broadcast(5, "<color=red>Zdradził*ś swoją drużynę!</color>");
        switch (player.Role.Type)
        {
            case RoleTypeId.Scientist:
                player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                break;
            case RoleTypeId.ClassD:
                player.Role.Set(RoleTypeId.Scientist, RoleSpawnFlags.AssignInventory);
                break;
            case RoleTypeId.ChaosConscript:
            case RoleTypeId.ChaosRifleman:
                player.Role.Set(RoleTypeId.NtfSergeant, RoleSpawnFlags.AssignInventory);
                break;
            case RoleTypeId.ChaosMarauder:
            case RoleTypeId.ChaosRepressor:
                player.Role.Set(RoleTypeId.NtfCaptain, RoleSpawnFlags.AssignInventory);
                break;
            case RoleTypeId.FacilityGuard:
                player.Role.Set(RoleTypeId.ChaosRifleman, RoleSpawnFlags.AssignInventory);
                break;
            case RoleTypeId.NtfPrivate:
            case RoleTypeId.NtfSergeant:
            case RoleTypeId.NtfSpecialist:
                player.Role.Set(RoleTypeId.ChaosRifleman, RoleSpawnFlags.AssignInventory);
                break;
            case RoleTypeId.NtfCaptain:
                List<RoleTypeId> roles = new List<RoleTypeId>
                {
                    RoleTypeId.ChaosMarauder,
                    RoleTypeId.ChaosRepressor
                };
                player.Role.Set(roles.RandomItem(), RoleSpawnFlags.AssignInventory);
                break;
        }
    }
}