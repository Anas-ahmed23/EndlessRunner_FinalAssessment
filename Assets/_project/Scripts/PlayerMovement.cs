using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Difficulty")]
    public DifficultySettings difficulty;   // Scriptable Object

    [Header("Movement")]
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    [Header("State")]
    [HideInInspector] public bool isAlive = true;

    [Header("Jumping")]
    public float jumpForce = 7f;
    public float gravity = 20f;

    private CharacterController controller;
    private Animator anim;

    private int targetLane = 1;
    private float verticalVelocity = 0f;
    private float currentForwardSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        currentForwardSpeed = difficulty.baseForwardSpeed;
    }

    void Update()
    {
        if (!isAlive) return;

        // =============================
        // DIFFICULTY → SPEED SCALING
        // =============================
        if (ScoreManager.Instance != null)
        {
            float difficultyMultiplier =
                (ScoreManager.Instance.GetScore() / 100f)
                * difficulty.speedIncreasePer100Score;

            currentForwardSpeed =
                difficulty.baseForwardSpeed + difficultyMultiplier;
        }

        // --------------------------
        // FORWARD MOVEMENT
        // --------------------------
        Vector3 move = Vector3.forward * currentForwardSpeed;

        // --------------------------
        // LANE SWITCHING
        // --------------------------
        if (Input.GetKeyDown(KeyCode.RightArrow) && targetLane < 2)
            targetLane++;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetLane > 0)
            targetLane--;

        float targetX = (targetLane - 1) * laneDistance;
        float deltaX = targetX - transform.position.x;
        move.x = deltaX * laneChangeSpeed;

        // --------------------------
        // JUMPING
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
        // APPLY MOVEMENT
        // --------------------------
        controller.Move(move * Time.deltaTime);
    }

    // =============================
    // STOP ON DEATH
    // =============================
    public void StopImmediate()
    {
        isAlive = false;
        currentForwardSpeed = 0f;
        verticalVelocity = 0f;

        controller.Move(Vector3.zero);
        anim.SetBool("IsRunning", false);

        if (anim.HasParameterOfType("Die", AnimatorControllerParameterType.Trigger))
            anim.SetTrigger("Die");
    }
}

// =============================
// SAFE ANIM PARAM CHECK
// =============================
public static class AnimatorExtensions
{
    public static bool HasParameterOfType(
        this Animator animator,
        string name,
        AnimatorControllerParameterType type)
    {
        foreach (var p in animator.parameters)
            if (p.type == type && p.name == name)
                return true;
        return false;
    }
}
