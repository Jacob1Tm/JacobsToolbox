using System.Collections.Generic;
using Exiled.API.Features;
using UnityEngine;

namespace JacobsToolbox.CoinFlipEffects;

public class PlayerScaleEffect
{
    private static readonly Vector3 DefaultScale = new(1.13f, 0.5f, 1.13f);

    public static Dictionary<string, object> DefaultOptions => new()
    {
        ["Scale"] = DefaultScale,
    };

    public static void Apply(Player ply)
    {
        ply.Scale = EffectOptions.GetVector3(nameof(PlayerScaleEffect), "Scale", DefaultScale);
    }
}
