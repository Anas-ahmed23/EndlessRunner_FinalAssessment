using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public TextMeshProUGUI timeText;

    private float timeSurvived;
    private bool running = true;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!running) return;

        timeSurvived += Time.deltaTime;
        timeText.text = "TIME: " + Mathf.FloorToInt(timeSurvived) + "s";
    }

    public void StopTimer()
    {
        running = false;
    }

    public int GetTime()
    {
        return Mathf.FloorToInt(timeSurvived);
    }

    public void ResetTimer()
    {
        timeSurvived = 0f;
        running = true;
    }
}
