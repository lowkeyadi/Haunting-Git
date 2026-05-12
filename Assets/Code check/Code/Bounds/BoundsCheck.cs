using UnityEngine;
using UnityEngine.AI;

public class BoundsCheck : MonoBehaviour
{
    private Bounds bounds;

    void Awake()
    {
        // reads the box size from the collider already on this GameObject
        bounds = GetComponent<BoxCollider>().bounds;
    }

    public Vector3 GetRandomPoint()
    {
        // try up to 20 times to land on walkable NavMesh
        for (int i = 0; i < 20; i++)
        {
            Vector3 candidate = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                bounds.center.y,
                Random.Range(bounds.min.z, bounds.max.z)
            );

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 1.5f, NavMesh.AllAreas))
                return hit.position;
        }

        return transform.position; //  just stay put
    }
}