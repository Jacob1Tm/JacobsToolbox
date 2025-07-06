using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class FakeCassieEffect
{
    private static readonly Dictionary<string, string> _scpNames = new()
    {
        { "1 7 3", "SCP-173"},
        { "9 3 9", "SCP-939"},
        { "0 9 6", "SCP-096"},
        { "0 7 9", "SCP-079"},
        { "0 4 9", "SCP-049"},
        { "1 0 6", "SCP-106"}
    };
    public static void Apply(Player ply)
    {
        var scpName = _scpNames.ToList().RandomItem();
                
        Cassie.MessageTranslated($"scp {scpName.Key} successfully terminated by automatic security system",
            $"{scpName.Value} successfully terminated by Automatic Security System.");
        ply.Broadcast(5,"Zabiłeś scp?");
    }
}