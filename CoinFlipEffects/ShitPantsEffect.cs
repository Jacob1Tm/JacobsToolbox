using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class ShitPantsEffect
{
    public static void Apply(Player ply)
    {
        ply.PlaceTantrum();
        ply.Broadcast(5, "<color=red>Kurwa, trzeba było wziąć jeszcze jedną parę gaci...</color>");
    }
}