using UnityEngine;

public class PlayerCollisionDetect : MonoBehaviour
{
    public float checkRadius = 0.6f;
    public float forwardOffset = 1f;
    public float heightOffset = 0.9f;
    public LayerMask obstacleLayer;
    public string obstacleTag = "Obstacle";

    private PlayerMovement movement;
    private Animator anim;
    private bool hit = false;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (hit) return;

        Vector3 checkPos = transform.position
                           + transform.forward * forwardOffset
                           + Vector3.up * heightOffset;

        Collider[] collisions = Physics.OverlapSphere(checkPos, checkRadius);

        foreach (var col in collisions)
        {
            if (col.CompareTag(obstacleTag))
            {
                Debug.Log("Player hit obstacle: " + col.name);

                hit = true;
                movement.isAlive = false;

                anim.SetBool("IsRunning", false);

                if (HasParam(anim, "Die"))
                    anim.SetTrigger("Die");

                return;
            }
        }
    }

    bool HasParam(Animator animator, string name)
    {
        foreach (var p in animator.parameters)
            if (p.name == name)
                return true;
        return false;
    }
}
