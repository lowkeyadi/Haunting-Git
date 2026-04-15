using UnityEngine;

public class PlayScareAnimation : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator MyRoatat;
  public string DisplayName { get; }
   public bool CanInteract;
    public void Interact()
    {
      
    }
    private void Awake()
    {
        
   
    }
}