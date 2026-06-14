using UnityEngine;

/// <summary>
/// The integrator. Listens for the player's "scare animation complete" signal,
/// then rewards every NPC that is currently within <see cref="scareRadius"/> of
/// its own active waypoint. NPCs out of range are simply ignored (failed scare).
/// Works for any number of NPCs — each is checked against its own waypoint.
/// </summary>
public class ScareProximityCheck : MonoBehaviour
{
    [Header("Source")]
    [Tooltip("The player's scare-animation event source. Auto-found on this GameObject if left empty.")]
    [SerializeField] private ScareAnimationEvent scareSource;

    [Header("Scare")]
    [Tooltip("How close an NPC must be to its active waypoint for the scare to land.")]
    [SerializeField] private float scareRadius = 3f;
    [Tooltip("Fear amount forwarded to FearMeter.RegisterScare.")]
    [SerializeField] private float scareAmount = 1f;

    private void Awake()
    {
        if (scareSource == null) scareSource = GetComponent<ScareAnimationEvent>();
    }

    private void OnEnable()
    {
        if (scareSource != null) scareSource.OnScareAnimationComplete += HandleScareComplete;
    }

    private void OnDisable()
    {
        if (scareSource != null) scareSource.OnScareAnimationComplete -= HandleScareComplete;
    }

    private void HandleScareComplete()
    {
        Vector3 source = transform.position;

        // Each NPC checks its own distance to its own current waypoint.
        foreach (FearMeter meter in FearMeter.Active)
        {
            if (meter == null) continue;

            var follower = meter.GetComponent<IWaypointFollower>();
            if (follower == null || !follower.HasWaypoint) continue;

            float distance = Vector3.Distance(meter.transform.position, follower.CurrentWaypoint);
            if (distance <= scareRadius)
                meter.RegisterScare(scareAmount, source);
        }
    }

#if UNITY_EDITOR
    // Visualise each NPC's success radius around its active waypoint while playing.
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        foreach (FearMeter meter in FearMeter.Active)
        {
            var follower = meter != null ? meter.GetComponent<IWaypointFollower>() : null;
            if (follower != null && follower.HasWaypoint)
                Gizmos.DrawWireSphere(follower.CurrentWaypoint, scareRadius);
        }
    }
#endif
}
