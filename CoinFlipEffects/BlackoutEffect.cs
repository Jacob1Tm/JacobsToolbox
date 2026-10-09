using System.Collections.Generic;
using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class BlackoutEffect
{
    public static Dictionary<string, object> DefaultOptions => new()
    {
        ["Duration"] = 10f,
    };

    public static void Apply(Player ply)
    {
        Map.TurnOffAllLights(EffectOptions.GetFloat(nameof(BlackoutEffect), "Duration", 10f));
        Map.Broadcast(5, "Ciemno wszędzie, głucho wszędzie, co to będzie? co to będzie?");
    }
}
