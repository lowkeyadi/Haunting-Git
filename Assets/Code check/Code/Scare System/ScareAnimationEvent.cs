using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sits on the Animator that plays the player's scare clip.
/// Add a Unity Animation Event on the final frame of the scare clip that calls
/// <see cref="NotifyScareComplete"/>. Listeners react to the event below instead
/// of polling the Animator every frame.
/// </summary>
public class ScareAnimationEvent : MonoBehaviour
{
    [Tooltip("Raised when the scare animation finishes — Inspector/UnityEvent hook.")]
    public UnityEvent OnScareAnimationCompleteEvent;

    /// C# event mirror of the above, for code-only listeners (e.g. ScareProximityCheck).
    public event Action OnScareAnimationComplete;

    /// <summary>
    /// Wire this up as an Animation Event on the last frame of the scare clip.
    /// </summary>
    public void NotifyScareComplete()
    {
        OnScareAnimationComplete?.Invoke();
        OnScareAnimationCompleteEvent?.Invoke();
    }
}
