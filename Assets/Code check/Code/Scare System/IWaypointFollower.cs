using UnityEngine;

/// <summary>
/// Implemented by anything that walks a waypoint path and can report which
/// waypoint it is currently heading to. Keeps the scare/proximity system
/// decoupled from the concrete movement script (EnemyCheck).
/// </summary>
public interface IWaypointFollower
{
    /// World-space position of the waypoint the follower is currently targeting.
    Vector3 CurrentWaypoint { get; }

    /// False until a path has been initialised, so listeners can skip the NPC.
    bool HasWaypoint { get; }
}
