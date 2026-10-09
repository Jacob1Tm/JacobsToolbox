using System.Collections.Generic;
using System.Linq;
using Exiled.API.Extensions;
using Exiled.API.Features;
using PlayerRoles;

namespace JacobsToolbox.CoinFlipEffects;

public static class SpectatorSwapEffect
{
    public static void Apply(Player player)
    {
        var spectList = Player.List.Where(x => x.Role.Type == RoleTypeId.Spectator || API.IsKomar(x)).ToList();
                
        if (spectList.IsEmpty())
        {
            CoinFlipEffect.CoinEffect(player);
            return;
        }
                
        var spect = spectList.RandomItem();
                
        spect.Role.Set(player.Role.Type, RoleSpawnFlags.None);
        spect.Teleport(player);
        spect.Health = player.Health;
                
        List<ItemType> playerItems = player.Items.Select(item => item.Type).ToList();

        foreach (var item in playerItems)
        {
            spect.AddItem(item);
        }
                
                
        //give spect the players ammo, has to be done before ClearInventory() or else ammo will fall on the floor
        for (int i = 0; i < player.Ammo.Count; i++)
        {
            spect.AddAmmo(player.Ammo.ElementAt(i).Key.GetAmmoType(), player.Ammo.ElementAt(i).Value);
            player.SetAmmo(player.Ammo.ElementAt(i).Key.GetAmmoType(), 0);
        }
                
        player.ClearInventory();
        player.Role.Set(RoleTypeId.Spectator);

        player.Broadcast(5, $"Zamieniłeś się z obserwatorem {spect.Nickname}!");
        spect.Broadcast(5, $"Zamieniłeś się z graczem {player.Nickname}!");
    }
}