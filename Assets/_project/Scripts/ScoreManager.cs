using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;      // Shows SCORE
    public TextMeshProUGUI highScoreText;  // Shows HIGH SCORE
    public Transform player;

    private float score = 0f;
    private bool isRunning = true;

    private int highScore = 0;

    void Awake()
    {
        Instance = this;

        // Load saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (!isRunning) return;

        // Score is based on Z distance
        score = player.position.z;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + Mathf.FloorToInt(score);

        if (highScoreText != null)
            highScoreText.text = "HIGH SCORE: " + highScore;
    }

    public void StopScore()
    {
        isRunning = false;

        int finalScore = Mathf.FloorToInt(score);

        // Save new high score if it's bigger
        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0;
        isRunning = true;
        UpdateUI();
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}
