using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(value);
            }

            Destroy(gameObject);
        }
    }
}
