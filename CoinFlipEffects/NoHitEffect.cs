using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class NoHitEffect
{
    public static Dictionary<string, object> DefaultOptions => new()
    {
        ["Instakill"] = true,
        ["PreventHealing"] = true,
    };

    public static void Apply(Player player)
    {
        if ((int) player.Health == 1 && EffectOptions.GetBool(nameof(NoHitEffect), "Instakill", true))
            player.Kill(DamageType.CardiacArrest);

        player.Health = 1;
        if (EffectOptions.GetBool(nameof(NoHitEffect), "PreventHealing", true)) 
            player.MaxHealth = 1;
        player.Broadcast(5, "Lecisz NOHIT!");
    }
}
