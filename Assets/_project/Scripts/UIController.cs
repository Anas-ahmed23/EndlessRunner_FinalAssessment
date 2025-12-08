using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.Instance) return;

        if (GameManager.Instance.isGameOver && !gameOverPanel.activeSelf)
        {
            // show panel
            gameOverPanel.SetActive(true);
            // optional: pause game time
            Time.timeScale = 0f;
        }
    }
}
