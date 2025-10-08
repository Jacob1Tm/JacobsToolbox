using System.ComponentModel;
using Discord;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using Exiled.API.Features;
using Round = LabApi.Features.Wrappers.Round;

namespace JacobsToolbox.Features;

public class AutoLobbyLock : CustomEventsHandler
{
    [Description("Enables or disables the Auto Lobby Lock function.")]
    public bool IsEnabled { get; set; } = true;

    public override void OnServerWaitingForPlayers()
    {
        if (!IsEnabled)
            return;
        Round.IsLobbyLocked = true;
    }
}