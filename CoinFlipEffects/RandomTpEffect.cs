using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;

namespace JacobsToolbox.CoinFlipEffects;

public class RandomTpEffect
{
    private static readonly HashSet<RoomType> DefaultRooms = new()
    {
        RoomType.EzCafeteria,
        RoomType.EzCheckpointHallwayA,
        RoomType.EzCheckpointHallwayB,
        RoomType.EzConference,
        RoomType.EzCrossing,
        RoomType.EzCurve,
        RoomType.EzDownstairsPcs,
        RoomType.EzGateA,
        RoomType.EzGateB,
        RoomType.EzIntercom,
        RoomType.EzPcs,
        RoomType.EzStraight,
        RoomType.EzTCross,
        RoomType.EzUpstairsPcs,
        RoomType.EzVent,
        RoomType.Hcz049,
        RoomType.Hcz079,
        RoomType.Hcz096,
        RoomType.Hcz106,
        RoomType.Hcz939,
        RoomType.HczArmory,
        RoomType.HczCrossing,
        RoomType.HczCurve,
        RoomType.HczElevatorA,
        RoomType.HczElevatorB,
        RoomType.HczEzCheckpointA,
        RoomType.HczEzCheckpointB,
        RoomType.HczHid,
        RoomType.HczNuke,
        RoomType.HczStraight,
        RoomType.HczTesla,
        RoomType.HczTestRoom,
        RoomType.Lcz173,
        RoomType.Lcz330,
        RoomType.Lcz914,
        RoomType.LczAirlock,
        RoomType.LczArmory,
        RoomType.LczCafe,
        RoomType.LczCheckpointA,
        RoomType.LczCheckpointB,
        RoomType.LczClassDSpawn,
        RoomType.LczCrossing,
        RoomType.LczCurve,
        RoomType.LczGlassBox,
        RoomType.LczPlants,
        RoomType.LczStraight,
        RoomType.LczTCross,
        RoomType.LczToilets,
        RoomType.Surface,
    };

    public static Dictionary<string, object> DefaultOptions => new()
    {
        ["RoomsToTeleport"] = new HashSet<RoomType>(DefaultRooms),
    };

    public static void Apply(Player player)
    {
        if (Warhead.IsDetonated)
        {
            player.Broadcast(5,"Trafiłeś... Nic");
            return;
        }

        HashSet<RoomType> rooms = EffectOptions.GetRoomSet(nameof(RandomTpEffect), "RoomsToTeleport", DefaultRooms);
        if (rooms.Count == 0)
        {
            player.Broadcast(5,"Trafiłeś... Nic");
            return;
        }

        player.Teleport(Room.Get(rooms.GetRandomValue()));
        player.Broadcast(5,"Gdzie ja jestem?");
    }
}
