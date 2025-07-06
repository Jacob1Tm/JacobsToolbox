using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class BlackoutEffect
{
    public static void Apply(Player ply)
    {
        Map.TurnOffAllLights(Plugin.Instance.Config.BlackoutDuration);
        Map.Broadcast(5, "Ciemno wszędzie, głucho wszędzie, co to będzie? co to będzie?");
    }
}