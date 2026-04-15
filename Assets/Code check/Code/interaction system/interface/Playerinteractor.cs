using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

//public class Playerinteractor : MonoBehaviour
//{
//    [SerializeField] private float radius = 2f;
//    [SerializeField] private LayerMask interactableLayers;/// layers that we want to be able to interact with 

//    private Collider[] buffer = new Collider[32];/// contains all the objects around the player, assign in the update/
//    private IInteractable focused;
//    private void Update()
//    {
//        IInteractable nearest = FindNearestInteractable();
//    }
//    private IInteractable FindNearestInteractable()
//    {//                      to get all obj around us  
//        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer, interactableLayers, QueryTriggerInteraction.Collide];
//        IInteractable nearest = null;
//        float bestDistSG = float.MaxValue;
//        for (int i = 0; i < count; i++)
//        {
//        //// will be null at first and also save the nearest distance which at first will be maz value.
//        Collider col = buffer[i];
//        if (col == null) continue;   /// go through each collider that is not null and try to get an interactable
//        IInteractable interactable = col.GetComponentInParent<IInteractable>();
//        if (interactable == null) continue;
//        if (!interactable.CanInteract()) continue;
//        float disSq = (col.transform.position - transform.position).sqrMagnitude;///checks if distance to the player is smaller than our currently nearest.
//        if (disSq> bestDistSG)
//            {
//                bestDistSG = disSq;
//                nearest = interactable;

//            }
    



