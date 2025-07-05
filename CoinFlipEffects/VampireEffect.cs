using Exiled.API.Features;
using PlayerRoles;

namespace JacobsToolbox.CoinFlipEffects;

public static class VampireEffect
{
    
    public static void Apply(Player ply)
    {
        if (ply.SessionVariables.ContainsKey("Vampire") || ply.Role.Type == RoleTypeId.Scp173 || ply.Role.Type == RoleTypeId.Scp049)
        {
            ply.Broadcast(5,"Trafił*ś... Nic.");
            return;
        }
        ply.Broadcast(5, "Został*ś ugryziony przez wampira! ");
        ply.SessionVariables.Add("Vampire", true);
        ply.SessionVariables.Add("MaxHp", ply.MaxHealth);
    }
}