using Respawning.Waves;

namespace JacobsToolbox.Patches
{
    using HarmonyLib;

    /// <summary>
    /// Allows respawn wave to start when all spectators are ghosts.
    /// </summary>
    [HarmonyPatch(typeof(WaveSpawner), nameof(WaveSpawner.CanBeSpawned))]
    internal class CheckSpawnablePatch
    {
        internal static void Postfix(ReferenceHub player, ref bool __result)
        {
            if (API.IsKomar(player))
            {
                __result = true;
            }
        }
    }
}