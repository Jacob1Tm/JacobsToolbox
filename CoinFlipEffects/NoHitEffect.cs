using Exiled.API.Enums;
using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class NoHitEffect
{
    public static void Apply(Player player)
    {
        if ((int) player.Health == 1 && Plugin.Instance.Config.Instakill)
            player.Kill(DamageType.CardiacArrest);

        player.Health = 1;
        if (Plugin.Instance.Config.PreventHealing) 
            player.MaxHealth = 1;
        player.Broadcast(5, "Lecisz NOHIT!");
    }
}