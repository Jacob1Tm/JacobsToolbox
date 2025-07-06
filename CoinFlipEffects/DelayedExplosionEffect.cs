using System;
using Exiled.API.Features;
using MEC;

namespace JacobsToolbox.CoinFlipEffects;

public class DelayedExplosionEffect
{
    public static void Apply(Player ply)
    {
        float time = UnityEngine.Random.Range(10f, 120f);
        Timing.CallDelayed(time, () =>
        {
            ply.Explode();
            ply.Kill("Zdradziecka moneta!");
        });
    }
}