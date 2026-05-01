using TMPro;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.5f, 0f);
    [SerializeField] private string keyHint = "[E]";

    /// private ref
    private Camera cam;
    private Transform target;
    private Canvas canvas;
    private RectTransform canvasRect;
    private RectTransform labelRect;// we want the prompt to stay hidden

    private void Awake()
    {

        cam = Camera.main;
        labelRect = label.rectTransform;
        canvas = label.GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        Hide();
    }
    void LateUpdate()
    {
        if (target == null) return;

        if (!label.gameObject.activeSelf)
        {
            label.gameObject.SetActive(true);
        }
        Vector3 worldPos = target.position + worldOffset;
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
        Camera UiCam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : cam;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, UiCam, out Vector2 localPoint))
        {
            labelRect.anchoredPosition = localPoint;
        }


    }

    public void Show(IInteractable interactable)
    {
        if (interactable==null)
        {
            Hide();
            return;
        }
        target = interactable.transform;
        label.text = $"{keyHint}{interactable.DisplayName}";
        label.gameObject.SetActive(true);
    }

    public void Hide()
    {
        label.gameObject.SetActive(false);
        target = null;

    }






}
