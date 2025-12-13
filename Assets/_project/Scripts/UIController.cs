using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalTimeText;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ShowGameOver(float timeSurvived)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (finalTimeText != null)
        {
            int seconds = Mathf.FloorToInt(timeSurvived);
            finalTimeText.text = "TIME: " + seconds + "s";
        }
    }
}
