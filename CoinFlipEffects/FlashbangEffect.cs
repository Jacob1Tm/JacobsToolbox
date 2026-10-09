using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace JacobsToolbox.CoinFlipEffects;

public class FlashbangEffect
{
    public static void Apply(Player player)
    {
        FlashGrenade flash = (FlashGrenade) Item.Create(ItemType.GrenadeFlash, player);
        flash.FuseTime = 1f;
        flash.SpawnActive(player.Position);
        player.Broadcast(5, "MOJE OCZY!");
    }
}