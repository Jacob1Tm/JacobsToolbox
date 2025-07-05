using Exiled.API.Features;
using Exiled.API.Features.Items;
using UnityEngine;

namespace JacobsToolbox.CoinFlipEffects;

public class LiveGrenadeEffect
{
    public static void Apply(Player player)
    {
        ExplosiveGrenade grenade = (ExplosiveGrenade) Item.Create(ItemType.GrenadeHE);
        grenade.FuseTime = (float) Plugin.Instance.Config.GrenadeFuseTime;
        grenade.SpawnActive(player.Position + Vector3.up, player);
        player.Broadcast(5, "Oops, wyślizgnął się! <color=red>UCIEKAJ!</color>");
    }
}