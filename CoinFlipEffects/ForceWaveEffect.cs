using Exiled.API.Features;
using Respawning;

namespace JacobsToolbox.CoinFlipEffects;

public class ForceWaveEffect
{
    public static void Apply(Player ply)
    {
        Respawn.ForceWave(WaveManager.Waves.RandomItem());
        ply.Broadcast(5, "Wezwałeś wsparcie... Tylko czy na pewno dobry oddział?");
    } 
}