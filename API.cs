using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Exiled.API.Features;

namespace JacobsToolbox
{
    public static class API
    {
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
    }
}