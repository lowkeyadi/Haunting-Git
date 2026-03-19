
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string displayName = "Interact";
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private UnityEvent onInteract;///// what why
    public string DisplayName => displayName;
    public bool CanInteract() => isEnabled;
    private Outline outline;
    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();////// ref for the scripts. no need to add the outline to each obj--- also in wake.
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.blue;//////Automatic scares.
        outline.OutlineWidth = 1f;
        outline.enabled = false;/// so it starts disabled


    }
    public void Interact()
    {
        //throw new System.NotImplementedException();
        onInteract?.Invoke();
    }

    public void OnFocusGained()
    {
        outline.enabled = true;
    }

    public void OnFocusLost()
    {
        outline.enabled = false;
    }
}
