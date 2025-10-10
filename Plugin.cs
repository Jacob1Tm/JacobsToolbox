using System;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using System.Collections.Generic;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using PlayerHandler = Exiled.Events.Handlers.Player;
using Scp173Handler = Exiled.Events.Handlers.Scp173;
using Scp939Handler = Exiled.Events.Handlers.Scp939;
using Scp096Handler = Exiled.Events.Handlers.Scp096;
using ServerHandler = Exiled.Events.Handlers.Server;
using Scp914Handler = Exiled.Events.Handlers.Scp914;
using MapHandler = Exiled.Events.Handlers.Map;
using UserSettings.ServerSpecific;
using LabApi.Events.CustomHandlers;
using HarmonyLib;
using UnityEngine;

namespace JacobsToolbox
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        private static Harmony _harmony;
        public Features.AutoLobbyLock AutoLobbyLock { get; } = new();
        public Features.StartVotingSystem.Events StartVotingSystem { get; } = new();

        public override void OnEnabled()
        {
            Instance = this;
            CustomHandlersManager.RegisterEventsHandler(AutoLobbyLock);
            CustomHandlersManager.RegisterEventsHandler(StartVotingSystem);
            Config.KomarRole.Register();
            
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
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            CustomRole.UnregisterRoles();
            CustomHandlersManager.UnregisterEventsHandler(AutoLobbyLock);
            CustomHandlersManager.UnregisterEventsHandler(StartVotingSystem);
            _harmony.UnpatchAll();
            base.OnDisabled();
        }
    }
}