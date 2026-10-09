using Exiled.API.Features;
using PlayerRoles;

namespace JacobsToolbox.CoinFlipEffects;

public class InventoryResetEffect
{
    public static void Apply(Player ply)
    {
        ply.ClearInventory();
        var hp = ply.Health;
        ply.Role.Set(ply.Role, RoleSpawnFlags.AssignInventory);
        ply.Health = hp; // Restore health after resetting inventory   
        ply.Broadcast(5, "<color=red>Gdzie moje przedmioty???</color>");
    }
}