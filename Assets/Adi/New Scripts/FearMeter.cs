using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class FearMeter : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private int maxState = 12;
    [SerializeField] private int startState = 1;

    [Header("Decay")]
    [SerializeField] private bool enableDecay = true;
    [SerializeField] private float decayTime = 5f;

    [Header("UI")]
    public FearMeterUI fearMeterUI;

    [Header("Scene Change")]
    public bool changeSceneAtMaxFear = true;
    public string nextSceneName;

    [Header("Events")]
    public UnityEvent<int> OnStateChanged;

    public event Action<float, Vector3> OnScareRegistered;

    private float decayTimer;
    private bool sceneChangeStarted = false;

    public int CurrentState { get; private set; }
    public int MaxState => maxState;

    private static readonly List<FearMeter> active = new List<FearMeter>();
    public static IReadOnlyList<FearMeter> Active => active;

    private void OnEnable()
    {
        active.Add(this);
    }

    private void OnDisable()
    {
        active.Remove(this);
    }

    private void Awake()
    {
        CurrentState = Mathf.Clamp(startState, 1, maxState);
        decayTimer = decayTime;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (!enableDecay)
            return;

        decayTimer -= Time.deltaTime;

        if (decayTimer <= 0f)
        {
            SetState(Mathf.Max(CurrentState - 1, 1));
            decayTimer = decayTime;
        }
    }

    public void RegisterScare()
    {
        RegisterScare(1f, transform.position);
    }

    public void RegisterScare(float amount, Vector3 source)
    {
        decayTimer = decayTime;
        SetState(CurrentState + 1);
        OnScareRegistered?.Invoke(amount, source);
    }

    private void SetState(int value)
    {
        int clamped = Mathf.Clamp(value, 1, maxState);

        if (clamped == CurrentState)
            return;

        CurrentState = clamped;

        Debug.Log($"{gameObject.name} Fear State: {CurrentState}");

        UpdateUI();
        OnStateChanged?.Invoke(CurrentState);

        if (CurrentState >= maxState)
        {
            GoToNextScene();
        }
    }

    private void UpdateUI()
    {
        if (fearMeterUI != null)
            fearMeterUI.UpdateFearUI(CurrentState);
    }

    private void GoToNextScene()
    {
        if (!changeSceneAtMaxFear)
            return;

        if (sceneChangeStarted)
            return;

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("Next Scene Name is empty on " + gameObject.name);
            return;
        }

        sceneChangeStarted = true;
        SceneManager.LoadScene(nextSceneName);
    }
}