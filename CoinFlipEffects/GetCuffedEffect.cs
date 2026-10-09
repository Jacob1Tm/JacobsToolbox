using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public static class GetCuffedEffect
{
    public static void Apply(Player ply)
    {
        ply.Handcuff();
        ply.DropItems();
        ply.Broadcast(5, "Admin nadużywa swoich permisji i cię zakuwa!");
    }
}