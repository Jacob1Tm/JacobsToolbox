using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class HpDebuffEffect
{
    public static void Apply(Player player)
    {
        player.Health *= 0.9f;
        player.MaxHealth *= 0.9f;
        player.Broadcast(5, "Your LOVE decreased");
    }
}