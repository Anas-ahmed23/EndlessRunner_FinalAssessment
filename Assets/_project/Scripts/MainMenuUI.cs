using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuCanvas;

    void Start()
    {
        // Pause game while in menu
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
