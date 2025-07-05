using System;
using System.Collections.Generic;
using Exiled.API.Features;
using JetBrains.Annotations;

namespace JacobsToolbox;

public static class CoinFlipEffect
{
    private static Config pconfig => Plugin.Instance.Config;
    public static readonly Dictionary<System.Action<Player>, int> Effects = new()
    {
        { CoinFlipEffects.ExplodeEffect.Apply, pconfig.ExplodeEffectChance },
        { CoinFlipEffects.ScpfyEffect.Apply , pconfig.ScpfyEffectChance },
        { CoinFlipEffects.NothingEffect.Apply, pconfig.NothingEffectChance},
        { CoinFlipEffects.BlindAustralianEffect.Apply, pconfig.BlindAustralianEffectChance },
        { CoinFlipEffects.WayOutEffect.Apply, pconfig.WayOutEffectChance},
        { CoinFlipEffects.OldMansTouch.Apply, pconfig.OldMansTouchEffectChance},
        { CoinFlipEffects.VampireEffect.Apply, pconfig.VampireEffectChance },
        { CoinFlipEffects.RoleSwapEffect.Apply, pconfig.RoleSwapEffectChance },
        { CoinFlipEffects.PlayerSwapEffect.Apply, pconfig.PlayerSwapEffectChance},
        { CoinFlipEffects.SpectatorSwapEffect.Apply, pconfig.SpectatorSwapEffectChance},
        { CoinFlipEffects.TeslaTeleportEffect.Apply, pconfig.TeslaTeleportEffectChance },
        { CoinFlipEffects.GetCuffedEffect.Apply, pconfig.GetCuffedEffectChance },
        { CoinFlipEffects.PinkCandySpawnEffect.Apply, pconfig.PinkCandySpawnEffectChance },
        { CoinFlipEffects.EmptyMicrohidEffect.Apply, pconfig.EmptyMicrohidEffectChance },
        { CoinFlipEffects.ForceWaveEffect.Apply, pconfig.ForceWaveEffectChance },
        { CoinFlipEffects.BlackoutEffect.Apply, pconfig.BlackoutEffectChance },
        { CoinFlipEffects.LiveGrenadeEffect.Apply, pconfig.LiveGrenadeEffectChance},
        { CoinFlipEffects.NoHitEffect.Apply, pconfig.NoHitEffectChance },
        { CoinFlipEffects.FlashbangEffect.Apply, pconfig.FlashbangEffectChance },
        { CoinFlipEffects.RandomItemEffect.Apply, pconfig.RandomItemEffectChance},
        { CoinFlipEffects.HpBoostEffect.Apply, pconfig.HpBoostEffectChance },
        { CoinFlipEffects.HpDebuffEffect.Apply, pconfig.HpDebuffEffectChance },
        { CoinFlipEffects.PlayerScaleEffect.Apply, pconfig.PlayerScaleEffectChance },
        { CoinFlipEffects.InventoryResetEffect.Apply, pconfig.InventoryResetEffectChance}
    };
    public static void CoinEffect(Player ply)
    {
        var effect = API.ChooseWeighted(Effects);
        effect(ply);
        Log.Debug( effect.Method.DeclaringType.FullName + " was applied to " + ply.Nickname);
    }
}