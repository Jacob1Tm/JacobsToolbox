using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace JacobsToolbox.CoinFlipEffects;

public class VaseEffect
{
    public static void Apply(Player player)
    {
        Scp244 vase = (Scp244)Item.Create(ItemType.SCP244a);
        vase.Primed = true;
        vase.CreatePickup(player.Position);
        player.Broadcast(5, "Znalazłeś wazę swojej babci!");
    }
}