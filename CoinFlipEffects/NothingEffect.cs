using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public static class NothingEffect
{
    public static void Apply(Player ply)
    {
        ply.Broadcast( 5, "Trafiłeś... Nic." );
    }
}