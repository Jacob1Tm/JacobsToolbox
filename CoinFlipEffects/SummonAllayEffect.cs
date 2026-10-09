using System;
using System.Linq;
using Exiled.API.Features;
using PlayerRoles;

namespace JacobsToolbox.CoinFlipEffects;

public class SummonAllayEffect
{
    public static void Apply(Player ply)
    {
        var spectList = Player.List.Where(x => x.Role.Type == RoleTypeId.Spectator || API.IsKomar(x)).ToList();
        if (spectList.Count == 0)
        {
            Log.Warn("No spectators found to summon Allay.");
            CoinFlipEffect.CoinEffect(ply);
            return;
        }
        var randomSpectator = spectList[new Random().Next(spectList.Count)];
        if (ply.Role.Team == Team.ChaosInsurgency || ply.Role.Team == Team.ClassD)
        {
            randomSpectator.Role.Set(RoleTypeId.ChaosRifleman);
            var position = ply.Position;
            randomSpectator.Position.Set(position.x, position.y, position.z);
        }
        else if (ply.Role.Team == Team.Scientists || ply.Role.Team == Team.FoundationForces)
        {
            randomSpectator.Role.Set(RoleTypeId.Scientist);
            var position = ply.Position;
            randomSpectator.Position.Set(position.x, position.y, position.z);
        }
        else if (ply.Role.Team == Team.SCPs)
        {
            randomSpectator.Role.Set(RoleTypeId.Scp0492);
            var position = ply.Position;
            randomSpectator.Position.Set(position.x, position.y, position.z);
        }
        else
        {
            Log.Warn("Unexpected player role for summoning Allay.");
            CoinFlipEffect.CoinEffect(ply);
            return;
        }
    }
}