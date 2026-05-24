using UnityEngine;

public class FearMeterUI : MonoBehaviour
{
    [Header("Fear States")]
    public GameObject[] states;

    [Header("Fear Labels")]
    public GameObject calmLabel;
    public GameObject nervousLabel;
    public GameObject scaredLabel;
    public GameObject terrifiedLabel;

    [Header("Settings")]
    [Range(1, 12)]
    public int currentFearState = 1;

    [Header("Auto Test")]
    public bool autoIncreaseFear = false;

    [Tooltip("How many seconds before fear increases automatically")]
    public float autoIncreaseDelay = 2f;

    private int lastFearState = -1;

    private float timer;

    private void Start()
    {
        UpdateFearUI();
    }

    private void Update()
    {
        // Detect inspector changes live
        if (currentFearState != lastFearState)
        {
            UpdateFearUI();
        }

        // AUTO TEST SYSTEM
        if (autoIncreaseFear)
        {
            timer += Time.deltaTime;

            if (timer >= autoIncreaseDelay)
            {
                timer = 0f;

                IncreaseFear();
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateFearUI();
    }
#endif

    // =========================
    // FEAR CONTROLS
    // =========================

    public void IncreaseFear()
    {
        currentFearState++;

        currentFearState = Mathf.Clamp(currentFearState, 1, 12);

        UpdateFearUI();
    }

    public void DecreaseFear()
    {
        currentFearState--;

        currentFearState = Mathf.Clamp(currentFearState, 1, 12);

        UpdateFearUI();
    }

    public void SetFear(int value)
    {
        currentFearState = Mathf.Clamp(value, 1, 12);

        UpdateFearUI();
    }

    // =========================
    // UI UPDATE
    // =========================

    void UpdateFearUI()
    {
        lastFearState = currentFearState;

        // Disable ALL states
        foreach (GameObject state in states)
        {
            if (state != null)
            {
                state.SetActive(false);
            }
        }

        // Enable CURRENT state
        if (states.Length >= currentFearState)
        {
            if (states[currentFearState - 1] != null)
            {
                states[currentFearState - 1].SetActive(true);
            }
        }

        // Disable all labels
        if (calmLabel) calmLabel.SetActive(false);
        if (nervousLabel) nervousLabel.SetActive(false);
        if (scaredLabel) scaredLabel.SetActive(false);
        if (terrifiedLabel) terrifiedLabel.SetActive(false);

        // Enable correct label
        if (currentFearState <= 3)
        {
            if (calmLabel) calmLabel.SetActive(true);
        }
        else if (currentFearState <= 6)
        {
            if (nervousLabel) nervousLabel.SetActive(true);
        }
        else if (currentFearState <= 9)
        {
            if (scaredLabel) scaredLabel.SetActive(true);
        }
        else
        {
            if (terrifiedLabel) terrifiedLabel.SetActive(true);
        }
    }
}