using System;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using System.Collections.Generic;
using PlayerHandler = Exiled.Events.Handlers.Player;
using Scp173Handler = Exiled.Events.Handlers.Scp173;
using Scp939Handler = Exiled.Events.Handlers.Scp939;
using Scp096Handler = Exiled.Events.Handlers.Scp096;
using ServerHandler = Exiled.Events.Handlers.Server;
using Scp914Handler = Exiled.Events.Handlers.Scp914;
using UserSettings.ServerSpecific;
using LabApi.Events.CustomHandlers;
using HarmonyLib;
using UnityEngine;

namespace JacobsToolbox
{
    public class Plugin : Plugin<Config>
    {
        private static Harmony _harmony;
        public static Plugin Instance;
        internal static IEnumerable<SettingBase> _settings;
        public EventHandlers.LabApiEvents Events { get;  } = new();

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
            CustomHandlersManager.RegisterEventsHandler(Events);
             PlayerHandler.Verified += EventHandlers.General.OnVerified;
             PlayerHandler.Died += EventHandlers.General.OnDeath;
             PlayerHandler.Left += EventHandlers.General.OnLeft;
             PlayerHandler.ChangingRole += EventHandlers.General.OnChangingRole;
             Scp173Handler.BeingObserved += EventHandlers.KomarEvents.OnBeingObserved;
             Scp096Handler.AddingTarget += EventHandlers.KomarEvents.OnAddingTarget;
             Scp939Handler.ValidatingVisibility += EventHandlers.KomarEvents.OnValidatingVisibility;
             Scp939Handler.PlayingFootstep += EventHandlers.KomarEvents.OnPlayingFootstep;
             PlayerHandler.RemovingHandcuffs += EventHandlers.KomarEvents.OnRemovingHandcuffs;
             Scp939Handler.SavingVoice += EventHandlers.KomarEvents.OnSavingVoice;
             PlayerHandler.TriggeringTesla += EventHandlers.KomarEvents.OnTriggeringTesla;
             PlayerHandler.IntercomSpeaking += EventHandlers.KomarEvents.OnIntercomSpeaking; 
             ServerHandler.RespawningTeam += EventHandlers.KomarEvents.OnRespawningTeam;
             Scp914Handler.UpgradingPickup += EventHandlers.General.OnUpgradingPickup;
             Scp914Handler.UpgradingInventoryItem += EventHandlers.General.OnUpgradingInventoryItem;
             ServerSpecificSettingsSync.ServerOnSettingValueReceived += EventHandlers.CustomKeybinds.KeybindExample;
             PlayerHandler.FlippingCoin += EventHandlers.General.OnFlippingCoin;
             PlayerHandler.Hurting += EventHandlers.General.OnHurting;

             _settings =
             [
                 new HeaderSetting("Jacob's Toolbox", "A collection of various features and enhancements for SCP:SL."),
                 new KeybindSetting(24, "Podnoszenie monet jako SCP", KeyCode.Z, hintDescription: "Pozwala graczowi na podniesienie monmety jako SCP po celowaniu na nią."),
             ];
             try
             {
                 _harmony = new Harmony(nameof(JacobsToolbox).ToLowerInvariant() + "-" + DateTime.UtcNow.Ticks);
                 _harmony.PatchAll();

                 Log.Info("Harmony patching complete.");
             }
             catch (Exception e)
             {
                 Log.Error($"Harmony patching failed! {e}");
             }

             SettingBase.Register(_settings);
        }

        public override void OnDisabled()
        {
            Instance = null;
            CustomHandlersManager.UnregisterEventsHandler(Events);
             PlayerHandler.Verified -= EventHandlers.General.OnVerified;
             PlayerHandler.Died -= EventHandlers.General.OnDeath;
             PlayerHandler.Left -= EventHandlers.General.OnLeft;
             PlayerHandler.ChangingRole -= EventHandlers.General.OnChangingRole;
             Scp173Handler.BeingObserved -= EventHandlers.KomarEvents.OnBeingObserved;
             Scp096Handler.AddingTarget -= EventHandlers.KomarEvents.OnAddingTarget;
             Scp939Handler.ValidatingVisibility -= EventHandlers.KomarEvents.OnValidatingVisibility;
             Scp939Handler.PlayingFootstep -= EventHandlers.KomarEvents.OnPlayingFootstep;
             PlayerHandler.RemovingHandcuffs -= EventHandlers.KomarEvents.OnRemovingHandcuffs;
             Scp939Handler.SavingVoice -= EventHandlers.KomarEvents.OnSavingVoice;
             PlayerHandler.TriggeringTesla -= EventHandlers.KomarEvents.OnTriggeringTesla;
             PlayerHandler.IntercomSpeaking -= EventHandlers.KomarEvents.OnIntercomSpeaking;
             ServerHandler.RespawningTeam -= EventHandlers.KomarEvents.OnRespawningTeam;
             Scp914Handler.UpgradingPickup -= EventHandlers.General.OnUpgradingPickup;
             Scp914Handler.UpgradingInventoryItem -= EventHandlers.General.OnUpgradingInventoryItem;
             ServerSpecificSettingsSync.ServerOnSettingValueReceived -= EventHandlers.CustomKeybinds.KeybindExample;
             PlayerHandler.FlippingCoin -= EventHandlers.General.OnFlippingCoin;
             PlayerHandler.Hurting -= EventHandlers.General.OnHurting;
             
             _harmony.UnpatchAll();
             _harmony = null;
            base.OnDisabled();
        }
    }
}