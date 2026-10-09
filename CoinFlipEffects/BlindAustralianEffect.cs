using CustomPlayerEffects;
using Exiled.API.Features;
using UnityEngine;

namespace JacobsToolbox.CoinFlipEffects;

public static class BlindAustralianEffect
{
    public static void Apply(Player ply)
    {
        ply.Broadcast(5, "<color=red>Ślepy australijczyk cię wzywa...</color>");
        ply.EnableEffect<Blurred>();
        ply.Scale = new Vector3(-1, -1, -1);
    }
}