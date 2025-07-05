using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class BlackoutEffect
{
    public static void Apply(Player ply)
    {
        Map.TurnOffAllLights(Plugin.Instance.Config.BlackoutDuration);
        ply.Broadcast(5, "Ciemno wszędzie, glucho wszędzie, co to będzie? co to będzie?");
    }
}