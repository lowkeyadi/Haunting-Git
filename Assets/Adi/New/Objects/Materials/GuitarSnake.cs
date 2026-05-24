using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerAnimationPlayer : MonoBehaviour
{
    public Animator animator;

    public string triggerName = "Play";

    public GameObject interactionIndicator;

    private bool playerInRange = false;

    private void Start()
    {
        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            animator.SetTrigger(triggerName);

            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionIndicator != null)
            {
                interactionIndicator.SetActive(false);
            }
        }
    }
}