using System.Collections;
using UnityEngine;

public class EnemyCheck : MonoBehaviour, IWaypointFollower
{
    [Header("Movement")]
    public float speed = 2f;
    public Transform pathHolder;

    [Header("Rotation")]
    public float rotationSpeed = 8f;

    [Header("References")]
    public Animator animator;
    public FearMeter fearMeter;

    [Header("Animator Parameters")]
    public string walkingBoolName = "IsWalking";
    public string scaredTriggerName = "Scared";

    [Header("Scare Pause")]
    public float scaredPauseTime = 2.5f;

    public Vector3 CurrentWaypoint { get; private set; }
    public bool HasWaypoint { get; private set; }

    private bool isScaredPaused = false;

    private void Start()
    {
        if (fearMeter == null)
            fearMeter = GetComponent<FearMeter>();

        if (pathHolder == null || pathHolder.childCount < 2)
        {
            Debug.LogError("EnemyCheck needs a pathHolder with at least 2 waypoints.");
            return;
        }

        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        int targetWaypointIndex = 0;

        while (true)
        {
            Transform targetWaypointTransform = pathHolder.GetChild(targetWaypointIndex);

            Vector3 targetPosition = targetWaypointTransform.position;
            targetPosition.y = transform.position.y;

            CurrentWaypoint = targetPosition;
            HasWaypoint = true;

            SetWalking(true);

            while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                if (!isScaredPaused)
                {
                    Vector3 direction = targetPosition - transform.position;
                    direction.y = 0f;

                    RotateTowardsDirection(direction);

                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        targetPosition,
                        speed * Time.deltaTime
                    );
                }

                yield return null;
            }

            SetWalking(false);

            Waypoint waypoint = targetWaypointTransform.GetComponent<Waypoint>();

            float waitTime = 2f;
            Transform lookTarget = null;
            TriggerAnimationPlayer linkedScareObject = null;

            if (waypoint != null)
            {
                waitTime = waypoint.waitTime;
                lookTarget = waypoint.lookTarget;
                linkedScareObject = waypoint.linkedScareObject;
            }

            bool alreadyScaredAtThisWaypoint = false;
            float timer = 0f;

            while (timer < waitTime)
            {
                if (!isScaredPaused)
                {
                    if (lookTarget != null)
                    {
                        Vector3 lookDirection = lookTarget.position - transform.position;
                        lookDirection.y = 0f;
                        RotateTowardsDirection(lookDirection);
                    }

                    if (linkedScareObject != null &&
                        linkedScareObject.IsScaring &&
                        !alreadyScaredAtThisWaypoint)
                    {
                        alreadyScaredAtThisWaypoint = true;
                        StartCoroutine(ReactToScare());
                    }

                    timer += Time.deltaTime;
                }

                yield return null;
            }

            targetWaypointIndex = (targetWaypointIndex + 1) % pathHolder.childCount;
        }
    }

    private IEnumerator ReactToScare()
    {
        isScaredPaused = true;
        SetWalking(false);

        if (fearMeter != null)
            fearMeter.RegisterScare();

        if (animator != null)
            animator.SetTrigger(scaredTriggerName);

        yield return new WaitForSeconds(scaredPauseTime);

        isScaredPaused = false;
    }

    private void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void SetWalking(bool isWalking)
    {
        if (animator != null)
            animator.SetBool(walkingBoolName, isWalking);
    }
}