using UserSettings.ServerSpecific;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using InventorySystem.Items.Coin;
using InventorySystem.Items.Pickups;
using UnityEngine;

namespace JacobsToolbox.EventHandlers;

public class CustomKeybinds
{
    public static void KeybindExample(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != 24 || !keybindSetting.SyncIsPressed)
            return;
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        Player ply = Player.Get(referenceHub);
        int hitboxLayer = LayerMask.NameToLayer("Hitbox");
        int layerMask = ~(1 << hitboxLayer);
        RaycastHit hit;
        if (Physics.Raycast(ply.CameraTransform.position, ply.CameraTransform.forward, out hit, 2f, layerMask))
        {
            ItemPickupBase targetPickup = hit.transform.GetComponent<ItemPickupBase>();
            if (targetPickup == null)
                return;
            Pickup pickup = Pickup.Get(targetPickup);
            if (pickup.Type == ItemType.Coin && ply.IsScp)
            {
                Log.Debug("Picking up coin as SCP");
                pickup.Destroy();
                ply.CurrentItem = Item.Create(ItemType.Coin);
            }
            
        }
    }
}