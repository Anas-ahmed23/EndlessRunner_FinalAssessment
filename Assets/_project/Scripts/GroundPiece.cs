using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    public Transform player;   // <-- IMPORTANT: Transform, not GameObject

    void Update()
    {
        if (player == null) return;

        // Destroy road piece after the player is far ahead
        if (player.position.z - transform.position.z > 40f)
        {
            Destroy(gameObject);
        }
    }
}
