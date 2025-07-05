using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;

namespace JacobsToolbox.CoinFlipEffects;

public static class OldMansTouch
{
    private static readonly Dictionary<Player, CoroutineHandle> _coroutines = new();
    
    public static void Apply(Player ply)
    {
        ply.Broadcast(5, "Czujsz się jakby staruszek cię dotknął...");
        ply.EnableEffect<Ghostly>();
        ply.EnableEffect<Slowness>(30);
        ply.SessionVariables.Add("OldMansTouch", true);
        var handle = Timing.RunCoroutine(DamageCoroutine(ply));
        _coroutines [ply] = handle;
    }
    public static void Remove(Player ply)
    {
        if (_coroutines.TryGetValue(ply, out var handle))
        {
            Timing.KillCoroutines(handle);
            _coroutines.Remove(ply);
        }
        
        ply.DisableEffect<Ghostly>();
        ply.DisableEffect<Slowness>();
        ply.SessionVariables.Remove("OldMansTouch");
    }
    public static IEnumerator<float> DamageCoroutine(Player ply)
    {
        for (;;) //repeat the following infinitely
        {
            if (ply.Health > 1f)
            {
                ply.Hurt(1f);    
            }
            yield return Timing.WaitForSeconds(2f); 
        }
    }
}