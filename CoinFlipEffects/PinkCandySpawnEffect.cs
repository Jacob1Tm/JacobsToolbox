using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using UnityEngine;

namespace JacobsToolbox.CoinFlipEffects;

public class PinkCandySpawnEffect
{
    public static void Apply(Player player)
    {
        Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
        candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);
        candy.CreatePickup(player.Position);
        player.Broadcast(5,"O! <color=#FF00FF>Cukierek</color>!");
    }
}