
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string displayName = "Interact";
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private UnityEvent onInteract;///// what why
    public string DisplayName => displayName;
    public bool CanInteract() => isEnabled;

    public string DisplayName => throw new System.NotImplementedException();

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void OnFocusGained()
    {
        throw new System.NotImplementedException();
    }

    public void OnFocusLost()
    {
        throw new System.NotImplementedException();
    }
}
