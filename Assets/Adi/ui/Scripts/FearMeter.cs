using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Discrete-state fear meter for a single NPC.
///  - Rises by one state on a successful scare (<see cref="RegisterScare()"/>).
///  - Decays by one state whenever the decay timer expires without a scare.
///  - Always clamped to the [0, maxState] range.
/// Put one of these on every NPC that can be scared.
/// </summary>
[DisallowMultipleComponent]
public class FearMeter : MonoBehaviour
{
    [Header("State")]
    [Tooltip("Highest fear state the meter can reach.")]
    [SerializeField] private int maxState = 5;
    [Tooltip("State the meter starts at when the scene loads.")]
    [SerializeField] private int startState = 0;

    [Header("Decay")]
    [Tooltip("When on, the meter loses one state every time the decay timer expires without a scare.")]
    [SerializeField] private bool enableDecay = true;
    [Tooltip("Seconds without a scare before the meter drops one state.")]
    [SerializeField] private float decayTime = 5f;

    [Header("Events")]
    [Tooltip("Fires with the new state whenever it changes — hook UI up here.")]
    public UnityEvent<int> OnStateChanged;

    /// <summary>
    /// Raised on every successful scare with (amount, source world position).
    /// A future NPCController can subscribe here so a single RegisterScare()
    /// call drives both the meter and the NPC's reaction.
    /// </summary>
    public event Action<float, Vector3> OnScareRegistered;

    private float decayTimer;

    /// Current discrete fear state. UI and other scripts read this.
    public int CurrentState { get; private set; }
    public int MaxState => maxState;

    // --- Per-NPC registry so the proximity check can find every live meter ---
    private static readonly List<FearMeter> active = new List<FearMeter>();
    public static IReadOnlyList<FearMeter> Active => active;

    private void OnEnable() => active.Add(this);
    private void OnDisable() => active.Remove(this);

    private void Awake()
    {
        CurrentState = Mathf.Clamp(startState, 0, maxState);
        decayTimer = decayTime;
    }

    private void Update()
    {
        if (!enableDecay) return;

        decayTimer -= Time.deltaTime;
        if (decayTimer <= 0f)
        {
            SetState(CurrentState - 1);   // drop one state (clamped at 0)
            decayTimer = decayTime;       // reset the timer either way
        }
    }

    /// Convenience overload for callers that don't care about amount/source.
    public void RegisterScare() => RegisterScare(1f, transform.position);

    /// <summary>
    /// Call this on a successful scare: raises the meter one state, resets the
    /// decay timer, and notifies listeners (e.g. an NPCController).
    /// </summary>
    public void RegisterScare(float amount, Vector3 source)
    {
        decayTimer = decayTime;            // a scare always refreshes the timer
        SetState(CurrentState + 1);
        OnScareRegistered?.Invoke(amount, source);
    }

    private void SetState(int value)
    {
        int clamped = Mathf.Clamp(value, 0, maxState);
        if (clamped == CurrentState) return;
        CurrentState = clamped;
        OnStateChanged?.Invoke(CurrentState);
    }
}
