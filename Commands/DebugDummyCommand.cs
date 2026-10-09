using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;

namespace JacobsToolbox.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DebugDummyCommand : ICommand
    {
        public string Command => "debugdummy";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Spawn/remove lobby-like debug dummies (Spectator NPCs). Usage: debugdummy <add <amount> | remove | removen <amount>>";

        private const int MaxSpawnPerCommand = 20;

        private const string Usage = "Usage: debugdummy <add <amount> | remove | removen <amount>>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            if (arguments.Count == 0)
            {
                response = Usage;
                return false;
            }

            string subcommand = arguments.ElementAt(0).ToLowerInvariant();
            switch (subcommand)
            {
                case "add":
                    return Add(arguments, out response);
                case "remove":
                    return RemoveAll(out response);
                case "removen":
                    return RemoveN(arguments, out response);
                default:
                    response = $"Unknown subcommand '{subcommand}'. {Usage}";
                    return false;
            }
        }

        private static bool Add(ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count < 2)
            {
                response = "Usage: debugdummy add <amount>";
                return false;
            }

            if (!int.TryParse(arguments.ElementAt(1), out int amount) || amount <= 0)
            {
                response = "Amount must be a positive number.";
                return false;
            }

            if (amount > MaxSpawnPerCommand)
            {
                response = $"You can spawn at most {MaxSpawnPerCommand} dummies per command.";
                return false;
            }

            for (int i = 0; i < amount; i++)
                API.SpawnDebugDummy();

            response = $"Spawned {amount} debug dummy(ies). Total NPCs: {Npc.List.Count}.";
            return true;
        }

        private static bool RemoveAll(out string response)
        {
            int count = Npc.List.Count;
            if (count == 0)
            {
                response = "There are no dummies to remove.";
                return false;
            }

            Npc.DestroyAll();
            response = $"Removed all {count} dummy(ies).";
            return true;
        }

        private static bool RemoveN(ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count < 2)
            {
                response = "Usage: debugdummy removen <amount>";
                return false;
            }

            if (!int.TryParse(arguments.ElementAt(1), out int amount) || amount <= 0)
            {
                response = "Amount must be a positive number.";
                return false;
            }

            var npcs = Npc.List.ToList();
            if (npcs.Count == 0)
            {
                response = "There are no dummies to remove.";
                return false;
            }

            int toRemove = Math.Min(amount, npcs.Count);
            for (int i = 0; i < toRemove; i++)
                npcs[npcs.Count - 1 - i].Destroy();

            response = $"Removed {toRemove} dummy(ies). Remaining NPCs: {Npc.List.Count}.";
            return true;
        }
    }
}
