using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;

namespace JacobsToolbox.CoinFlipEffects;

public class ScpTpEffect
{
    public static void Apply(Player player)
    {
        if (Player.Get(Side.Scp).Any(x => x.Role.Type != RoleTypeId.Scp079))
        {
            Player scpPlayer = Player.Get(Side.Scp).Where(x => x.Role.Type != RoleTypeId.Scp079).ToList().RandomItem();
            player.Position = scpPlayer.Position;
            player.Broadcast(5, "Przywitaj się z kolegą!");
            return;
        }
        CoinFlipEffect.CoinEffect(player);
    }
}