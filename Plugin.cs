using System;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using System.Collections.Generic;
using Exiled.CustomRoles.API;
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
        public Features.AutoLobbyLock AutoLobbyLock { get; } = new();
        public Features.StartVotingSystem.Events StartVotingSystem { get; } = new();

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
            CustomHandlersManager.RegisterEventsHandler(AutoLobbyLock);
            CustomHandlersManager.RegisterEventsHandler(StartVotingSystem);
        }

        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
        }
    }
}