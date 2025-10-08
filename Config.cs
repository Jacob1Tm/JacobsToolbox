using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using UnityEngine;

namespace JacobsToolbox
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public Features.AutoLobbyLock AutoLobbyLock { get; set; } = new();
        public Features.StartVotingSystem StartVotingSystem { get; set; } = new();
    }
}