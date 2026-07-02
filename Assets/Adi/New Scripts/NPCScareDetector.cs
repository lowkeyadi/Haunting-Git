using UnityEngine;

public class NPCScareDetector : MonoBehaviour
{
    [Header("Detection")]
    public float detectionDistance = 5f;
    public LayerMask scareObjectLayer;

    [Header("References")]
    public FearMeter fearMeter;

    [Header("Cooldown")]
    public float scareCooldown = 2f;

    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        Ray ray = new Ray(transform.position + Vector3.up * 1f, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, detectionDistance, scareObjectLayer))
        {
            TriggerAnimationPlayer scareObject = hit.collider.GetComponent<TriggerAnimationPlayer>();

            if (scareObject != null && scareObject.IsScaring)
            {
                if (fearMeter != null)
                {
                    fearMeter.RegisterScare();
                    cooldownTimer = scareCooldown;
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.red);
    }
}