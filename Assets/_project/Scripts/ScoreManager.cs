using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("References")]
    public Transform player;

    private int score = 0;
    private int highScore = 0;
    private bool isRunning = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
    }

    void Update()
    {
        if (!isRunning || player == null) return;

        // Distance-based score (only increases)
        int distanceScore = Mathf.FloorToInt(player.position.z);
        score = Mathf.Max(score, distanceScore);

        UpdateUI();
    }

    // ⭐ Called by coins / collectibles
    public void AddScore(int amount)
    {
        if (!isRunning) return;

        score += amount;
        UpdateUI();
    }

    public void StopScore()
    {
        isRunning = false;

        // Save high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGH_SCORE", highScore);
            PlayerPrefs.Save();
        }
    }

    public void ResetScore()
    {
        score = 0;
        isRunning = true;
        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + score;

        if (highScoreText != null)
            highScoreText.text = "HIGHEST SCORE: " + highScore;
    }
}
