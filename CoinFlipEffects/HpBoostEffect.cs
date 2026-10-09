using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class HpBoostEffect
{
    public static void Apply(Player player)
    {
        player.Health *= 1.1f;
        player.Broadcast(5, "Your LOVE incresed.");
    }
}