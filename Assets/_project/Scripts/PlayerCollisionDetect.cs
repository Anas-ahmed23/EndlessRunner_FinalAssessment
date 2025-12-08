using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCollisionDetect : MonoBehaviour
{
    public float checkRadius = 0.6f;
    public float forwardOffset = 0.9f;
    public float heightOffset = 0.9f;
    public LayerMask obstacleLayer;
    public string obstacleTag = "Obstacle";

    private PlayerMovement movement;
    private Animator anim;
    private bool hitAlready = false;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        if (obstacleLayer.value == 0) obstacleLayer = ~0; // if not set, check all layers
    }

    void Update()
    {
        if (hitAlready) return;

        Vector3 localOffset = new Vector3(0f, heightOffset, forwardOffset);
        Vector3 worldPos = transform.TransformPoint(localOffset);

        Collider[] hits = Physics.OverlapSphere(worldPos, checkRadius, obstacleLayer, QueryTriggerInteraction.Ignore);
        if (hits != null && hits.Length > 0)
        {
            // fallback tag check
            foreach (var c in hits)
            {
                if (c.CompareTag(obstacleTag))
                {
                    DoHit(c.gameObject);
                    return;
                }
            }
        }
    }

    void DoHit(GameObject obstacle)
    {
        if (hitAlready) return;
        hitAlready = true;

        Debug.Log("Player hit obstacle! -> " + obstacle.name);

        if (movement != null)
        {
            movement.StopImmediate();
        }

        if (anim != null && HasParam(anim, "Die"))
            anim.SetTrigger("Die");

        // notify central manager (UI will read this)
        if (GameManager.Instance != null)
            GameManager.Instance.GameOver();
    }

    bool HasParam(Animator animator, string name)
    {
        foreach (var p in animator.parameters)
            if (p.name == name) return true;
        return false;
    }
}
