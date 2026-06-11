using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FORoutine());
    }

    private IEnumerator FORoutine()
    {
        
        WaitForSeconds Wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return Wait;
            FieldOfViewCheck();
        }
    }
        private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0) 
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward , directionToTarget)< angle/2)
            {
                float distanceToRarget= Vector3.Distance(transform.position, target.position);
                ////positive check first
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToRarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;

            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer= false;
    }

    }

