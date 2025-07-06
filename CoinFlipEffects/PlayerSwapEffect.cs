using Exiled.API.Features;
using System.Linq;
using PlayerRoles;

namespace JacobsToolbox.CoinFlipEffects;

public static class PlayerSwapEffect
{
    public static void Apply(Player player)
    {
        Log.Debug($"Applying PlayerSwapEffect to {player.Nickname} ({player.UserId})");
        foreach (var p in Player.List)
        {
            Log.Debug($"Player: {p.Nickname}, IsAlive: {p.IsAlive}, Role: {p.Role.Type}");
        }
        var playerList = Player.List.Where(x => x.IsAlive && x.Role.Type != RoleTypeId.Tutorial && x.Role.Type != RoleTypeId.Scp079).ToList();
        playerList.Remove(player);
                
        if (playerList.IsEmpty())
        {
            Log.Debug("No other players available for swap.");
            CoinFlipEffect.CoinEffect(player);
            return;
        }

        var targetPlayer = playerList.RandomItem();
        Log.Debug($"Selected target player for swap: {targetPlayer.Nickname} ({targetPlayer.UserId})");
        var pos = targetPlayer.Position;
                
        targetPlayer.Teleport(player.Position);
        player.Teleport(pos);
        player.Broadcast(5, $"Zamienił*ś się miejscami z <color=yellow>{targetPlayer.Nickname}</color>!");
        targetPlayer.Broadcast(5, $"Zamienił*ś się miejscami z <color=yellow>{player.Nickname}</color>!");        
    }
}