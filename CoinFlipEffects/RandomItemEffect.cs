using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace JacobsToolbox.CoinFlipEffects;

public class RandomItemEffect
{
    public static void Apply(Player player)
    {
        Item.Create(Plugin.Instance.Config.ItemsToGive.ToList().RandomItem()).CreatePickup(player.Position);
        player.Broadcast(5, $"Dostałeś losowy przedmiot!");
    }
}