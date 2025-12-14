using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 10;
    public GameObject pickupFX;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Add score
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(value);

        // Spawn particle effect
        if (pickupFX != null)
            Instantiate(pickupFX, transform.position, Quaternion.identity);

        // Play sound
        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1f);

        Destroy(gameObject);
    }
}
