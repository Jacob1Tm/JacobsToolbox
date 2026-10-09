using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace JacobsToolbox.CoinFlipEffects;

public class RandomItemEffect
{
    private static readonly HashSet<ItemType> DefaultItems = new()
    {
        ItemType.Adrenaline,
        ItemType.Coin,
        ItemType.Flashlight,
        ItemType.Jailbird,
        ItemType.Medkit,
        ItemType.Painkillers,
        ItemType.Radio,
        ItemType.ArmorCombat,
        ItemType.ArmorHeavy,
        ItemType.ArmorLight,
        ItemType.GrenadeFlash,
        ItemType.GrenadeHE,
        ItemType.GunA7,
        ItemType.GunCom45,
        ItemType.GunCrossvec,
        ItemType.GunLogicer,
        ItemType.GunRevolver,
        ItemType.GunShotgun,
        ItemType.GunAK,
        ItemType.GunCOM15,
        ItemType.GunCOM18,
        ItemType.GunE11SR,
        ItemType.GunFSP9,
        ItemType.GunFRMG0,
    };

    public static Dictionary<string, object> DefaultOptions => new()
    {
        ["ItemsToGive"] = new HashSet<ItemType>(DefaultItems),
    };

    public static void Apply(Player player)
    {
        HashSet<ItemType> items = EffectOptions.GetItemSet(nameof(RandomItemEffect), "ItemsToGive", DefaultItems);
        if (items.Count == 0)
        {
            player.Broadcast(5, "Trafiłeś... Nic.");
            return;
        }

        Item.Create(items.ToList().RandomItem()).CreatePickup(player.Position);
        player.Broadcast(5, $"Dostałeś losowy przedmiot!");
    }
}
