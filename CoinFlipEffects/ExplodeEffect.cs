using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public static class ExplodeEffect
{
    public static void Apply(Player ply)
    {
        ply.Explode();
        ply.Kill("Uzależnienie od hazardu");
        ply.Broadcast(5, "<color=red>BOOM!</color>", Broadcast.BroadcastFlags.Normal);
        
    }
}