using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using UnityEngine;

namespace JacobsToolbox
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Sets the time in seconds before spectator can become a Komar after death.")]
        public int KomarRoleCooldown { get; set; } = 10;

        [Description("Prevents upgrading SCP-2176 to coins on 1:1, used for limiting the amount of coins in the game.")]
        public bool Prevent2176Upgrade { get; set; } = true;

        [Description("Enables/disables removal of units from NTF")]
        public bool RemoveUnits { get; set; } = true;

        [Description("Should Mystery Coin's be enabled?")]
        public bool EnableMysteryCoins { get; set; } = true;

        [Description("Weighted chances for Mystery Coins Effects to activate")]
        public int ExplodeEffectChance { get; set; } = 5;
        public int ScpfyEffectChance { get; set; } = 1;
        public int NothingEffectChance { get; set; } = 5;
        public int BlindAustralianEffectChance { get; set; } = 1;
        public int WayOutEffectChance { get; set; } = 1;
        public int OldMansTouchEffectChance { get; set; } = 1;
        public int VampireEffectChance { get; set; } = 1;
        public int RoleSwapEffectChance { get; set; } = 1;
        public int PlayerSwapEffectChance { get; set; } = 1;
        public int SpectatorSwapEffectChance { get; set; } = 1;
        public int TeslaTeleportEffectChance { get; set; } = 1;
        public int GetCuffedEffectChance { get; set; } = 1;
        public int PinkCandySpawnEffectChance { get; set; } = 1;
        public int EmptyMicrohidEffectChance { get; set; } = 1;
        public int ForceWaveEffectChance { get; set; } = 1;
        public int BlackoutEffectChance { get; set; } = 1;
        [Description("Time of the blackout effect in seconds.")]
        public float BlackoutDuration { get; set; } = 10;
        public int LiveGrenadeEffectChance { get; set; } = 1;
        [Description("Explosion time of the grenade.")]
        public double GrenadeFuseTime { get; set; } = 3.25;
        
        [Description("Should player be instakilled if already has 1 hp (NoHit effect).")]
        public bool Instakill { get; set; } = true;
        public int NoHitEffectChance { get; set; } = 1;
        [Description("If true sets max health to 1 from NoHitEffect")]
        public bool PreventHealing { get; set; } = true;
        public int FlashbangEffectChance { get; set; } = 1;
        public int RandomItemEffectChance { get; set; } = 1;
        [Description("List of items that can be given to players by Random Item Effect.")]
        public HashSet<ItemType> ItemsToGive { get; set; } = new()
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

        public int HpBoostEffectChance { get; set; } = 1;
        public int HpDebuffEffectChance { get; set; } = 1;

        public int PlayerScaleEffectChance { get; set; } = 1;
        public Vector3 PlayerScale { get; set; } = new(1.13f, 0.5f, 1.13f);
        public int InventoryResetEffectChance { get; set; } = 1;
    }
}