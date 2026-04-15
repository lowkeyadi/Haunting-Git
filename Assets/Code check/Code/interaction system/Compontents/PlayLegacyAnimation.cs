using UnityEngine;

public class AnimationToggle : MonoBehaviour
{
    [SerializeField] private Animation animationComponent;
    [SerializeField] private string clipName = "Rotation_test";

    private bool isOn = false;

    private void Awake()
    {
        if (animationComponent == null)
            animationComponent = GetComponent<Animation>();
    }

    public void Toggle()
    {
        if (animationComponent == null)
        {
            Debug.LogWarning("No Animation component on " + gameObject.name);
            return;
        }

        isOn = !isOn;

        if (isOn)
        {
            animationComponent.Play(clipName);
        }
    }
}