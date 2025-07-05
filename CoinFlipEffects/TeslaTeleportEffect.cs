using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public static class TeslaTeleportEffect
{
    public static void Apply(Player player)
    {
        player.Teleport(Exiled.API.Features.TeslaGate.List.ToList().RandomItem());
                
        if (Warhead.IsDetonated)
        {
            player.Kill(DamageType.Decontamination);
            player.Broadcast(5,
                "Zostałeś teleportowany do <color=#00FFF6>Tesli</color>, ale niestety <color=red> warhead wybuchł</color> i zginąłeś.");
            return;
        }
        player.Broadcast(5,
            "Zostałeś teleportowany do <color=#00FFF6>Tesli</color>!");
    }
}