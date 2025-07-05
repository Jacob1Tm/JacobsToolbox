using System.Collections.Generic;
using PlayerRoles;
using Exiled.API.Features;
using UnityEngine;
using Random = System.Random;

namespace JacobsToolbox.CoinFlipEffects;


public static class ScpfyEffect
{
    private static readonly Dictionary<RoleTypeId, string> Scps = new()
    {
        { RoleTypeId.Scp049, "You are now SCP-049. You can use your abilities to heal and revive players." },
        { RoleTypeId.Scp0492, "You are now SCP-049-2. You can assist SCP-049 in reviving players." },
        { RoleTypeId.Scp096, "You are now SCP-096. You can become enraged and attack players who look at you." },
        { RoleTypeId.Scp173, "You are now SCP-173. You can move quickly and attack players who are not looking at you." },
        { RoleTypeId.Scp939, "You are now SCP-939. You can mimic player voices and attack them." }
    };

    private static readonly Random _random = new();

    public static void Apply(Player ply)
    {
        int index = _random.Next(Scps.Count);
        var scp = new List<KeyValuePair<RoleTypeId, string>>(Scps)[index];

        // Change player's role
        ply.Scale = new Vector3(1, 1, 1);
        ply.Role.Set(scp.Key, RoleSpawnFlags.None);

        // Broadcast the message to the player
        ply.Broadcast(10, scp.Value);
    }
}