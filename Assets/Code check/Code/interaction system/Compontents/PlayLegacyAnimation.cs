using UnityEngine;

public class PlayLegacyAnimation : MonoBehaviour
{
    [SerializeField] private Animation animationComponent;
    [SerializeField] private string Rotation_test;

    private void Awake()
    {
        if (animationComponent == null)
            animationComponent = GetComponent<Animation>();
    }

    public void PlayAnimation()
    {
        if (animationComponent == null)
        {
            Debug.LogWarning("No Animation component found on " + gameObject.name);
            return;
        }

        if (string.IsNullOrEmpty(Rotation_test))
        {
            Debug.LogWarning("Clip name is empty on " + gameObject.name);
            return;
        }

        Debug.Log("Playing animation: " + Rotation_test);  ///("Playing animation: " + clipName);
        animationComponent.Play(Rotation_test);
    }
}
