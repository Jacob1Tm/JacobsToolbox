using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using Exiled.API.Features;
using InventorySystem.Items.Keycards;
using PlayerRoles;
using UnityEngine;
using UserSettings.ServerSpecific;
using Utf8Json.Resolvers.Internal;
using VoiceChat;
using Player = LabApi.Features.Wrappers.Player;

namespace JacobsToolbox.EventHandlers
{

    public class LabApiEvents : CustomEventsHandler
    {
        public override void OnPlayerReceivingVoiceMessage(PlayerReceivingVoiceMessageEventArgs ev)
        {
            //Komar's can hear each other
            if (API.IsKomar(ev.Player) && API.IsKomar(ev.Sender) && ev.Sender != ev.Player)
            {
                ev.Message.Channel = VoiceChatChannel.RoundSummary;
                ev.IsAllowed = true;
            }

            //Komar's can hear spectators
            if (ev.Sender.Role == RoleTypeId.Spectator && API.IsKomar(ev.Player) && ev.Sender != ev.Player && API.IsOnSpectatorVC(ev.Player))
            {
                ev.Message.Channel = VoiceChatChannel.RoundSummary;
                ev.IsAllowed = true;
            }

            //Spectators can hear komar's
            if (API.IsKomar(ev.Sender) && ev.Player.Role == RoleTypeId.Spectator && ev.Sender != ev.Player && API.IsOnSpectatorVC(ev.Sender))
            {
                ev.Message.Channel = VoiceChatChannel.RoundSummary;
                ev.IsAllowed = true;
            }
        }
    }
}