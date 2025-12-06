using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    [Header("State")]
    [HideInInspector]
    public bool isAlive = true;

    [Header("Jumping")]
    public float jumpForce = 7f;
    public float gravity = 20f;

    private CharacterController controller;
    private Animator anim;

    private int targetLane = 1;      // 0 = left / 1 = middle / 2 = right
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAlive)
            return;

        // --------------------------
        //  FORWARD MOVEMENT
        // --------------------------
        Vector3 move = Vector3.forward * forwardSpeed;

        // --------------------------
        //  LANE SWITCHING
        // --------------------------
        if (Input.GetKeyDown(KeyCode.RightArrow) && targetLane < 2)
            targetLane++;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetLane > 0)
            targetLane--;

        float targetX = (targetLane - 1) * laneDistance;
        float deltaX = targetX - transform.position.x;
        move.x = deltaX * laneChangeSpeed;

        // --------------------------
        //  JUMPING
        // --------------------------
        if (controller.isGrounded)
        {
            anim.SetBool("IsRunning", true);

            if (verticalVelocity < 0)
                verticalVelocity = -2f;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                verticalVelocity = jumpForce;
                anim.SetTrigger("Jump");
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        // --------------------------
        //  APPLY MOVEMENT
        // --------------------------
        controller.Move(move * Time.deltaTime);
    }

    // ================================================================
    //  STOP IMMEDIATELY WHEN COLLIDING WITH AN OBSTACLE
    // ================================================================
    public void StopImmediate()
    {
        isAlive = false;

        // completely freeze motion
        forwardSpeed = 0f;
        verticalVelocity = 0f;

        // ensure no movement applied
        controller.Move(Vector3.zero);

        // stop running animation
        anim.SetBool("IsRunning", false);

        // safely trigger Die animation if it exists
        foreach (var p in anim.parameters)
        {
            if (p.name == "Die")
            {
                anim.SetTrigger("Die");
                break;
            }
        }
    }
}
