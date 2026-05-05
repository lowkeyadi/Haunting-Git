using UnityEngine;

public class PlayScareAnimation : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator MyRoatat;
    [SerializeField] private string displayName = "Scare Object";
    [SerializeField] private bool canInteract = true;
    private bool isOn = false;
    private object currentInteractable;

    public string DisplayName => displayName;

    private void Awake()
    {
        if (MyRoatat == null)
            MyRoatat = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ///////////////////////////////////////////////////////
            Debug.Log("E key detected");
            // ...whatever calls the interactable
            Debug.Log($"Current focused interactable: {currentInteractable}");
            /////////////////////////////////////////////////////////////////////////
            //Interact();
        }
    }

    public void Interact()
    {
        if (!canInteract || MyRoatat == null)
            return;

       isOn = !isOn;
       if (isOn)
       {
            MyRoatat.SetTrigger("Scare");
       }
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public void OnFocusGained() { }
    public void OnFocusLost() { }
}