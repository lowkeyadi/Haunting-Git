using UnityEngine;

public class FearMeterUI : MonoBehaviour
{
    public GameObject[] states;

    public GameObject calm;
    public GameObject nervous;
    public GameObject scared;
    public GameObject terrified;

    private void Start()
    {
        UpdateFearUI(1);
    }

    public void UpdateFearUI(int state)
    {
        for (int i = 0; i < states.Length; i++)
            states[i].SetActive(i == state - 1);

        calm.SetActive(state >= 1 && state <= 3);
        nervous.SetActive(state >= 4 && state <= 6);
        scared.SetActive(state >= 7 && state <= 9);
        terrified.SetActive(state >= 10 && state <= 12);
    }
}