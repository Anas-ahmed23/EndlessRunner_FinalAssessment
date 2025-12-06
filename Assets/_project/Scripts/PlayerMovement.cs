using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    public float jumpForce = 7f;
    public float gravity = 20f;

    private CharacterController controller;
    private Animator anim;

    private int targetLane = 1;  // Middle lane
    private float verticalVelocity = 0f;
    private Vector3 moveVector;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- ALWAYS MOVE FORWARD ---
        moveVector.z = forwardSpeed;

        // --- LANE SWITCHING ---
        if (Input.GetKeyDown(KeyCode.RightArrow) && targetLane < 2)
            targetLane++;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetLane > 0)
            targetLane--;

        float targetX = (targetLane - 1) * laneDistance;
        float deltaX = targetX - transform.position.x;

        moveVector.x = deltaX * laneChangeSpeed;

        // --- JUMP LOGIC ---
        if (controller.isGrounded)
        {
            anim.SetBool("IsRunning", true);

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

        moveVector.y = verticalVelocity;

        // --- APPLY MOVEMENT ---
        controller.Move(moveVector * Time.deltaTime);
    }
}
