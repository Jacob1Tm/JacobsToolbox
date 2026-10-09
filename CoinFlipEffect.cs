using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exiled.API.Features;
using MEC;

namespace JacobsToolbox;

public static class CoinFlipEffect
{
    public const string EffectsNamespace = "JacobsToolbox.CoinFlipEffects";

    private static readonly List<DiscoveredEffect> _effects = new();

    public sealed class DiscoveredEffect
    {
        public string Name;
        public Action<Player> Apply;
        public int DefaultWeight;
        public Dictionary<string, object> DefaultOptions;
    }

    /// <summary>
    /// Pure reflection scan, no Plugin/Config/Log access, so Config's constructor can use it
    /// to pre-fill defaults. Sorted by name for stable config output.
    /// </summary>
    public static List<DiscoveredEffect> Discover()
    {
        var found = new List<DiscoveredEffect>();
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.Namespace != EffectsNamespace || type.IsInterface)
                continue;

            MethodInfo method = type.GetMethod("Apply", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(Player) }, null);
            if (method == null || method.ReturnType != typeof(void))
                continue;

            int weight = 1;
            PropertyInfo weightProp = type.GetProperty("DefaultWeight", BindingFlags.Public | BindingFlags.Static);
            if (weightProp?.GetValue(null) is int w)
                weight = w;

            var options = new Dictionary<string, object>();
            PropertyInfo optionsProp = type.GetProperty("DefaultOptions", BindingFlags.Public | BindingFlags.Static);
            if (optionsProp?.GetValue(null) is Dictionary<string, object> d)
                options = new Dictionary<string, object>(d);

            found.Add(new DiscoveredEffect
            {
                Name = type.Name,
                Apply = (Action<Player>)Delegate.CreateDelegate(typeof(Action<Player>), method),
                DefaultWeight = weight,
                DefaultOptions = options,
            });
        }

        found.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
        return found;
    }

    /// <summary>
    /// (Re)discovers all coin flip effects. Dropping a new effect file into the
    /// CoinFlipEffects folder registers it automatically, including its default
    /// weight (<c>DefaultWeight</c>) and extra options (<c>DefaultOptions</c>).
    /// Missing config entries are filled with defaults (admin overrides are kept).
    /// </summary>
    public static void Reload()
    {
        _effects.Clear();
        _effects.AddRange(Discover());

        Config config = Plugin.Instance?.Config;
        if (config != null)
        {
            if (config.CoinEffectWeights == null)
                config.CoinEffectWeights = new Dictionary<string, int>();
            if (config.CoinEffectOptions == null)
                config.CoinEffectOptions = new Dictionary<string, Dictionary<string, object>>();

            foreach (DiscoveredEffect effect in _effects)
            {
                if (!config.CoinEffectWeights.ContainsKey(effect.Name))
                    config.CoinEffectWeights[effect.Name] = effect.DefaultWeight;

                if (effect.DefaultOptions.Count == 0)
                    continue;

                if (!config.CoinEffectOptions.TryGetValue(effect.Name, out var existing) || existing == null)
                {
                    config.CoinEffectOptions[effect.Name] = new Dictionary<string, object>(effect.DefaultOptions);
                }
                else
                {
                    foreach (var pair in effect.DefaultOptions)
                    {
                        if (!existing.ContainsKey(pair.Key))
                            existing[pair.Key] = pair.Value;
                    }
                }
            }
        }

        Log.Debug($"Loaded {_effects.Count} coin flip effects: {string.Join(", ", _effects.Select(e => e.Name))}");
    }

    private static int GetWeight(DiscoveredEffect effect)
    {
        Config config = Plugin.Instance?.Config;
        if (config?.CoinEffectWeights != null && config.CoinEffectWeights.TryGetValue(effect.Name, out int weight))
            return weight;
        return effect.DefaultWeight;
    }

    public static void CoinEffect(Player ply)
    {
        if (_effects.Count == 0)
            Reload();

        var weights = new Dictionary<Action<Player>, int>();
        foreach (DiscoveredEffect effect in _effects)
        {
            int weight = GetWeight(effect);
            if (weight > 0)
                weights[effect.Apply] = weight;
        }

        if (weights.Count == 0)
        {
            Log.Debug("No coin flip effects available (all weights <= 0).");
            return;
        }

        Action<Player> picked = API.ChooseWeighted(weights);
        picked(ply);
        Log.Debug(picked.Method.DeclaringType.FullName + " was applied to " + ply.Nickname);
    }

    /// <summary>
    /// Rolls and applies a random weighted effect after <paramref name="delay"/> seconds,
    /// so it lands when the coin flip animation finishes instead of at flip start.
    /// </summary>
    public static void TriggerDelayed(Player ply, float delay)
    {
        Timing.CallDelayed(delay, () =>
        {
            if (ply == null || !ply.IsConnected)
                return;
            CoinEffect(ply);
        });
    }
}
