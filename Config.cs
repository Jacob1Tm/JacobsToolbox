using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace JacobsToolbox
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public Features.AutoLobbyLock AutoLobbyLock { get; set; } = new();
        public Features.StartVotingSystem StartVotingSystem { get; set; } = new();
        public Features.KomarRole KomarRole { get; set; } = new();

        [Description("Should Mystery Coin's be enabled?")]
        public bool EnableMysteryCoins { get; set; } = true;

        [Description("Delay in seconds between flipping the coin and applying the random effect (waits for the flip animation to finish).")]
        public float CoinFlipDelay { get; set; } = 0.5f;

        [Description("Weighted chances per coin flip effect (effect name -> weight). Set a weight to 0 to disable that effect. Entries for all discovered effects are added automatically with their defaults.")]
        public Dictionary<string, int> CoinEffectWeights { get; set; } = new();

        [Description("Extra per-effect options (effect name -> option name -> value). Options registered by effects are added automatically with their defaults.")]
        public Dictionary<string, Dictionary<string, object>> CoinEffectOptions { get; set; } = new();

        public Config()
        {
            // Pre-fill defaults so a freshly generated config file already lists every effect.
            foreach (var effect in CoinFlipEffect.Discover())
            {
                CoinEffectWeights[effect.Name] = effect.DefaultWeight;
                if (effect.DefaultOptions.Count > 0)
                    CoinEffectOptions[effect.Name] = new Dictionary<string, object>(effect.DefaultOptions);
            }
        }
    }
}