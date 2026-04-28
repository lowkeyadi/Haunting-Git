
using UnityEngine;

public class Playerinteractor : MonoBehaviour
{
    [SerializeField] private float radius = 2f;
    [SerializeField] private LayerMask interactableLayers;/// layers that we want to be able to interact with 

    private Collider[] buffer = new Collider[32];/// contains all the objects around the player, assign in the update/
    private IInteractable focused;
    private void Update()
    {
        IInteractable nearest = FindNearestInteractable();
        UpdateFocus(nearest);
        if (focused != null && Input.GetKeyDown(KeyCode.E))
        {
          if(focused.CanInteract()) focused.Interact();
        }
    }
    private IInteractable FindNearestInteractable()
    {//                      to get all obj around us  
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, buffer, interactableLayers, QueryTriggerInteraction.Collide);
        Debug.Log($"[Interactor] Found {count} colliders in radius"); //// didnt work cuz barney is a very weird obj that has skinned mesh??
        IInteractable nearest = null;
        float bestdistsg = float.MaxValue;
        for (int i = 0; i < count; i++)
        {
            //// will be null at first and also save the nearest distance which at first will be maz value.
            Collider col = buffer[i]; 
            ///////////////////////////////////////////////////////////////////////////////////////////
            Debug.Log($"[Interactor] Checking collider: {col.gameObject.name}");
            ///////////////////////////////////////////////////////////////////////////////////////////
            if (col == null) continue;  /// go through each collider that is not null and try to get an interactable
            IInteractable interactable = col.GetComponent<IInteractable>();
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Debug.Log($"[Interactor] IInteractable found on {col.gameObject.name}? {interactable != null}");
           ///////////////////////////////////////////////////////////////////////////////////////////////////
            if (interactable == null) continue;
            if (!interactable.CanInteract()) continue;
            float dissq = (col.transform.position - transform.position).sqrMagnitude;///checks if distance to the player is smaller than our currently nearest.
            if (dissq < bestdistsg)
            {
                bestdistsg = dissq;
                nearest = interactable;

            }

        }
        return nearest;
    }
    private void UpdateFocus(IInteractable nearest) 
    {
        if (ReferenceEquals(focused, nearest)) return;
        ///////////////////////////////////////////////////////
        Debug.Log($"[Interactor] Focus changed. Old: {focused}, New: {nearest}");
        //////////////////////////////////////////////////////////////////////////
        focused?.OnFocusLost();
        focused = nearest;
        focused?.OnFocusGained();
    }
}

