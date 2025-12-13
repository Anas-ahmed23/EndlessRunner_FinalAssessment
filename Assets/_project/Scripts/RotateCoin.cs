using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    [Header("Rotation")]
    public float rotateSpeed = 180f;

    [Header("Floating (Visual Polish)")]
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);

        // Float up & down
        transform.position = startPos +
            Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatHeight;
    }
}
