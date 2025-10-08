using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Exiled.API.Features;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Utilities;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace JacobsToolbox.Features;

public class StartVotingSystem
{
    [Description("Enables or disables the Start Voting System.")]
    public bool IsEnabled { get; set; } = true;
    public  string VotingPassedMessage { get; set; } = "The vote has passed! Disabling the Lobby Lock.";
    
    public string CommandDescription { get; set; } =
        "Starts a voting system to vote for ready players to start the round.";

    public string CounterHintText { get; set; } = "Ready Players: ";
    
    [Description("Configure the Hint")]
    public int CounterHintTextSize { get; set; } = 40;
    public int CounterHintYCoordinate { get; set; } = 5;
    public string VotedMessage { get; set; } = "You are now ready.";
    public string UnvotedMessage { get; set; } = "You are no longer marked as ready.";

    [Description("Percentage of players needed to vote to start the round. (0.0 - 1.0)")]
    public float VotePercentage { get; set; } = 0.9f;
    
    private static List<Player> _readyPlayers = new();
    private static int _votesNeeded;

    private static Hint _voteCounter = new()
    {
        Text = "Ready Players: 0/0 (0%)",
        Alignment = HintAlignment.Center,
        YCoordinateAlign = HintVerticalAlign.Top,
    };

    private static void UpdateCounter()
    {
        
        var allPlayers = Player.List.Count;
        _votesNeeded = (int)Math.Ceiling(allPlayers * Plugin.Instance.Config.StartVotingSystem.VotePercentage);
        var currentVotes = _readyPlayers.Count;
        var currentPercentage = allPlayers == 0 ? 0 : (float)currentVotes / allPlayers;
        _voteCounter.Text = Plugin.Instance.Config.StartVotingSystem.CounterHintText + $"{currentVotes}/{_votesNeeded} ({currentPercentage:P0})" + "\n.r w konsoli aby zagłosować";
        if (currentVotes >= _votesNeeded)
        {
            _voteCounter.Text = Plugin.Instance.Config.StartVotingSystem.VotingPassedMessage;
            Round.IsLobbyLocked = false;
        }
        else if (!Round.IsLobbyLocked)
        {
            Round.IsLobbyLocked = true;
        }
    }
    
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ReadyCommand : ICommand
    {
        public string Command => "ready";

        public string[] Aliases =>
        [
            "r",
            "v"
        ];
        
        public string Description => Plugin.Instance.Config.StartVotingSystem.CommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            var player = Player.Get(sender);
            if (!Plugin.Instance.Config.StartVotingSystem.IsEnabled)
            {
                response = "The voting system is disabled.";
                return false;
            }

            if (Round.IsStarted)
            {
                response = "The round has already started.";
                return false;
            }

            if (_readyPlayers.Contains(player))
            {
                _readyPlayers.Remove(player);
                response = Plugin.Instance.Config.StartVotingSystem.UnvotedMessage;
                UpdateCounter();
                return true;
            }

            _readyPlayers.Add(player);
            UpdateCounter();
            response = Plugin.Instance.Config.StartVotingSystem.VotedMessage;
            return true;
        }
    }

    public class Events : CustomEventsHandler
    {
        public override void OnServerWaitingForPlayers()
        {
            _voteCounter.YCoordinate = Plugin.Instance.Config.StartVotingSystem.CounterHintYCoordinate;
            _voteCounter.FontSize = Plugin.Instance.Config.StartVotingSystem.CounterHintTextSize;
            _readyPlayers.Clear();
            UpdateCounter();
        }

        public override void OnServerRoundStarting(RoundStartingEventArgs ev)
        {
            foreach (var player in Player.List)
            {
                var playerDisplay = PlayerDisplay.Get(player);
                playerDisplay.RemoveHint(_voteCounter);
            }
        }

        public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
        {
            var playerDisplay = PlayerDisplay.Get(ev.Player);
            playerDisplay.AddHint(_voteCounter);
            UpdateCounter();
        }

        public override void OnPlayerLeft(PlayerLeftEventArgs ev)
        {
            _readyPlayers.Remove(Player.Get(ev.Player));
            UpdateCounter();
        }
    }
}