using UnityEngine;
using UnityEngine.SceneManagement;

public class DiningRoomFearMeter : MonoBehaviour
{
    [Header("State")]
    public int currentState = 1;
    public int maxState = 12;

    [Header("Decay")]
    public bool enableDecay = true;
    public float decayTime = 10f;

    [Header("UI")]
    public FearMeterUI fearMeterUI;

    [Header("Scene Change")]
    public bool changeSceneAtMaxFear = true;
    public string nextSceneName;

    private float decayTimer;
    private bool sceneChangeStarted = false;

    private void Start()
    {
        currentState = Mathf.Clamp(currentState, 1, maxState);
        decayTimer = decayTime;
        UpdateUI();
    }

    private void Update()
    {
        if (!enableDecay || sceneChangeStarted)
            return;

        decayTimer -= Time.deltaTime;

        if (decayTimer <= 0f)
        {
            currentState = Mathf.Max(currentState - 1, 1);
            UpdateUI();
            decayTimer = decayTime;
        }
    }

    public void RegisterScare()
    {
        if (sceneChangeStarted)
            return;

        decayTimer = decayTime;

        currentState = Mathf.Clamp(currentState + 1, 1, maxState);
        UpdateUI();

        if (currentState >= maxState)
        {
            GoToNextScene();
        }
    }

    private void UpdateUI()
    {
        if (fearMeterUI != null)
            fearMeterUI.UpdateFearUI(currentState);
    }

    private void GoToNextScene()
    {
        if (!changeSceneAtMaxFear)
            return;

        if (string.IsNullOrEmpty(nextSceneName))
            return;

        sceneChangeStarted = true;
        SceneManager.LoadScene(nextSceneName);
    }
}