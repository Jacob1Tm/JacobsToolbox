using LabApi.Features.Wrappers;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace JacobsToolbox.CoinFlipEffects;

public static class WayOutEffect
{
    public static void Apply(Player ply)
    {
        ply.Broadcast(5, ply.Nickname + ", Stay Determined!");
        ply.CurrentItem = null;
        ply.Handcuff();
        ply.Position = new Vector3(-5.2f, 301, 1);
    }
}