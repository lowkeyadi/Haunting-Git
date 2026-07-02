using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Waypoint Settings")]
    public float waitTime = 2f;

    [Tooltip("The NPC will look at this while waiting.")]
    public Transform lookTarget;

    [Header("Scare Check")]
    public TriggerAnimationPlayer linkedScareObject;
}