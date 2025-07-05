using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class PlayerScaleEffect
{
    public static void Apply(Player ply)
    {
        ply.Scale = Plugin.Instance.Config.PlayerScale;
    }
}