using UnityEngine;

/// <summary>
/// Interface for any NPC that follows waypoints.
/// It allows other scripts to know which waypoint the NPC
/// is currently walking towards.
/// </summary>
public interface IWaypointFollower
{
    /// <summary>
    /// The world position of the waypoint the NPC is currently heading to.
    /// </summary>
    Vector3 CurrentWaypoint { get; }

    /// <summary>
    /// Returns true once the NPC has been assigned a waypoint.
    /// </summary>
    bool HasWaypoint { get; }
}