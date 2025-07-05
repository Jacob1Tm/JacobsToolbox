using Exiled.API.Features;
using Exiled.API.Features.Pickups;

namespace JacobsToolbox.CoinFlipEffects;

public class EmptyMicrohidEffect
{
    public static void Apply(Player player)
    {
        MicroHIDPickup item = (MicroHIDPickup)Pickup.Create(ItemType.MicroHID);
        item.Position = player.Position;
        item.Spawn();
        item.Energy = 0;
        player.Broadcast(5, "<color=red>Execute order 66</color>");
    }
}