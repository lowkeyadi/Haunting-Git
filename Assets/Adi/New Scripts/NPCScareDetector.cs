using UnityEngine;

public class NPCScareDetector : MonoBehaviour
{
    [Header("Detection")]
    public float detectionDistance = 5f;
    public LayerMask scareObjectLayer;

    [Header("References")]
    public FearMeter fearMeter;
    public Animator animator;

    [Header("Animator")]
    public string scaredTriggerName = "Scared";

    [Header("Cooldown")]
    public float scareCooldown = 2f;

    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        Ray ray = new Ray(transform.position + Vector3.up * 1f, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, detectionDistance, scareObjectLayer))
        {
            TriggerAnimationPlayer scareObject =
                hit.collider.GetComponentInParent<TriggerAnimationPlayer>();

            if (scareObject != null && scareObject.IsScaring)
            {
                if (fearMeter != null)
                    fearMeter.RegisterScare();

                if (animator != null)
                    animator.SetTrigger(scaredTriggerName);

                cooldownTimer = scareCooldown;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.red);
    }
}