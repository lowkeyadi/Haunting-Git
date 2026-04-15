using UnityEngine;

public class PlayScareAnimation : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator MyRoatat;
    [SerializeField] private string displayName = "Scare Object";
    [SerializeField] private bool canInteract = true;
    private bool isOn = false;

    public string DisplayName => displayName;

    private void Awake()
    {
        if (MyRoatat == null)
            MyRoatat = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (!canInteract || MyRoatat == null)
            return;

        isOn = !isOn;
        MyRoatat.SetBool("Scare", isOn);
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public void OnFocusGained() { }
    public void OnFocusLost() { }
}