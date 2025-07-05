using Exiled.API.Extensions;
using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class RandomTpEffect
{
    public static void Apply(Player player)
    {
        if (Warhead.IsDetonated)
        {
            player.Broadcast(5,"Trafiłeś... Nic");
            return;
        }
        player.Teleport(Room.Get(Plugin.Instance.Config.RoomsToTeleport.GetRandomValue()));
        player.Broadcast(5,"Gdzie ja jestem?");
    }
}