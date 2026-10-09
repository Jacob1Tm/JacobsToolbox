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

        private bool _debugDummySpawned;

        public override void OnEnabled()
        {
            Instance = this;
            CustomHandlersManager.RegisterEventsHandler(AutoLobbyLock);
            CustomHandlersManager.RegisterEventsHandler(StartVotingSystem);
            Config.KomarRole.Register();

            PlayerHandler.Verified += OnPlayerVerified;
            PlayerHandler.FlippingCoin += OnFlippingCoin;
            ServerHandler.WaitingForPlayers += OnWaitingForPlayers;
            CoinFlipEffect.Reload();

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
            PlayerHandler.Verified -= OnPlayerVerified;
            PlayerHandler.FlippingCoin -= OnFlippingCoin;
            ServerHandler.WaitingForPlayers -= OnWaitingForPlayers;

            Instance = null;
            CustomRole.UnregisterRoles();
            CustomHandlersManager.UnregisterEventsHandler(AutoLobbyLock);
            CustomHandlersManager.UnregisterEventsHandler(StartVotingSystem);
            _harmony.UnpatchAll();
            base.OnDisabled();
        }

        private void OnFlippingCoin(Exiled.Events.EventArgs.Player.FlippingCoinEventArgs ev)
        {
            if (!Config.EnableMysteryCoins)
                return;

            CoinFlipEffect.TriggerDelayed(ev.Player, Config.CoinFlipDelay);
        }

        private void OnWaitingForPlayers()
        {
            _debugDummySpawned = false;
        }

        private void OnPlayerVerified(Exiled.Events.EventArgs.Player.VerifiedEventArgs ev)
        {
            // Only act on the first real player joining the lobby.
            if (_debugDummySpawned)
                return;

            if (Npc.List.Count != 0)
                return;

            // Player.List includes NPCs, so first real join == exactly 1 entry.
            if (Player.List.Count != 1)
                return;

            _debugDummySpawned = true;

            // Create a lobby-like dummy using the EXILED API function.
            API.SpawnDebugDummy("DebugDummy");
            Log.Debug($"Spawned debug dummy for first player {ev.Player.Nickname} ({ev.Player.UserId}).");

            // Enable round lock if debug is enabled.
            if (Config.Debug)
            {
                Round.IsLocked = true;
                Log.Debug("Round lock enabled (Debug mode).");
            }
        }
    }
}
