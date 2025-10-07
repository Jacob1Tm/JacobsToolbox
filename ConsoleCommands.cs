using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.Permissions.Extensions;
using InventorySystem.Items.Pickups;
using PlayerRoles;
using UnityEngine;

namespace JacobsToolbox
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Explode : ICommand
    {
        public string Command => "explode";
        public string Description => "you explode";
        public string[] Aliases => new[] { "ex", "boom", "EXPLOSION" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            if (!ply.CheckPermission("explode.self"))
            {
                response = "You don't have permissions to explode yourself Missing permission: explode.self";
                return false;
            }

            if (ply.IsDead)
            {
                response = "you can't explode yourself while being dead.";
                return false;
            }

            ply.Explode();
            ply.Kill("You Exploded");
            response = "explode";
            return true;
        }
    }
}
