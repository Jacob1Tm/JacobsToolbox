using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using PlayerRoles;

namespace JacobsToolbox
{
    public static class API
    {
        private static int _debugDummyCounter;

        /// <summary>
        /// Spawns a lobby-like debug dummy: Spectator role, NOT ignored, default spawn position,
        /// so it behaves as close to a freshly joined lobby player as possible.
        /// </summary>
        public static Npc SpawnDebugDummy(string name)
        {
            return Npc.Spawn(name, RoleTypeId.Spectator);
        }

        /// <summary>
        /// Spawns a lobby-like debug dummy with an auto-generated unique name.
        /// </summary>
        public static Npc SpawnDebugDummy()
        {
            return SpawnDebugDummy($"DebugDummy-{++_debugDummyCounter}");
        }

        public static T ChooseWeighted<T>(Dictionary<T, int> weights)
        {
            if (weights is null || weights.Count == 0)
                throw new ArgumentException("Weights dictionary is null or empty.");

            int totalWeight = 0;
            foreach (var weight in weights.Values)
                totalWeight += weight;

            int randomValue = new Random().Next(totalWeight);
            int cumulative = 0;

            foreach (var pair in weights)
            {
                cumulative += pair.Value;
                if (randomValue < cumulative)
                    return pair.Key;
            }

            throw new InvalidOperationException("No items to choose from.");
        }

        public static bool IsKomar(Player player)
        {
            foreach (var customRole in player.GetCustomRoles())
            {
                if (customRole.Id == Plugin.Instance.Config.KomarRole.Id)
                    return true;
            }

            return false;
        }
    }
}
