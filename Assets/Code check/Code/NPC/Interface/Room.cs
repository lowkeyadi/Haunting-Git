using UnityEngine;
using UnityEngine.AI;


public class Room : MonoBehaviour
{
    [Header("Setup")]
    public Room assignedRoom;

    [Header("Wander Settings")]
    public float waitMin = 1f;
    public float waitMax = 3f;

    protected NavMeshAgent agent;
    private float waitTimer;

    protected virtual void  Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNewSpot();
    }
    private void Update()
    {
        /// agent finished walking?
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)

        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
                GoToNewSpot();
        }
    }
    void GoToNewSpot()
    {
        //agent.SetDestination(assignedRoom.GetRandomPoint());
        waitTimer = Random.Range(waitMin, waitMax);
    }

    }
 