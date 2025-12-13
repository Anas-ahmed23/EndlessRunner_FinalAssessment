using UnityEngine;
using TMPro;
using System.Collections;

public class DifficultyUI : MonoBehaviour
{
    public TextMeshProUGUI difficultyText;
    private int lastShownLevel = 0;

    void Update()
    {
        if (ScoreManager.Instance == null) return;

        int currentLevel = ScoreManager.Instance.GetScore() / 100;

        if (currentLevel > lastShownLevel)
        {
            lastShownLevel = currentLevel;
            StartCoroutine(ShowDifficulty());
        }
    }

    IEnumerator ShowDifficulty()
    {
        difficultyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        difficultyText.gameObject.SetActive(false);
    }
}
