using System.Linq;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Scp173;
using Exiled.Events.EventArgs.Scp939;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles.Spectating;
using VoiceChat;

namespace JacobsToolbox.EventHandlers
{
    public static class KomarEvents
    {
        public static void OnAddingTarget(AddingTargetEventArgs ev)
        {
            // Check if the target has the KomarRole custom role
            if (ev.Target.SessionVariables.ContainsKey("IsKomar") || ev.Target.SessionVariables.ContainsKey("NoInteraction"))
            {
                ev.IsAllowed = false;
            }
        }
        public static void OnBeingObserved(BeingObservedEventArgs ev)
        {
            // Check if the observer has the KomarRole custom role
            if (ev.Target.SessionVariables.ContainsKey("IsKomar") || ev.Target.SessionVariables.ContainsKey("NoInteraction"))
            {
                ev.IsAllowed = false;
            }
        }
        public static void OnValidatingVisibility(ValidatingVisibilityEventArgs ev)
        {
            // Check if the target has the KomarRole custom role
            if (ev.Target.SessionVariables.ContainsKey("IsKomar") || ev.Target.SessionVariables.ContainsKey("NoInteraction"))
            {
                ev.IsAllowed = false;
            }
        }
        public static void OnPlayingFootstep(PlayingFootstepEventArgs ev)
        {
            // Check if the player has the KomarRole custom role
            if (ev.Target.SessionVariables.ContainsKey("IsKomar") || ev.Target.SessionVariables.ContainsKey("NoInteraction"))
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            // Check if the target has the KomarRole custom role
            Log.Debug("Removing handcuffs event triggered for: " + ev.Target.Nickname);
            if (ev.Target.SessionVariables.ContainsKey("IsKomar"))
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnSavingVoice(SavingVoiceEventArgs ev)
        {
            if (ev.Stolen.SessionVariables.ContainsKey("IsKomar") || ev.Stolen.SessionVariables.ContainsKey("NoInteraction"))
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsKomar") || ev.Player.SessionVariables.ContainsKey("NoInteraction"))
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsKomar") || ev.Player.SessionVariables.ContainsKey("NoInteraction"))
            {
                ev.IsAllowed = false;
            }
        }

        public static void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            ev.Players.AddRange(Player.Get(API.IsKomar));
        }

        public static void OnPlayerSpawningRagdoll(PlayerSpawningRagdollEventArgs ev)
        {
            if (API.IsKomar(ev.Player))
                
            {
                ev.IsAllowed = false;
            }
        }
        
        // public static void OnPlayerDying(DiedEventArgs ev)
        // {
        //     if (API.IsKomar(ev.Player))
        //     {
        //         ev.Attacker != null; 
        //     }
        // }
    }
}