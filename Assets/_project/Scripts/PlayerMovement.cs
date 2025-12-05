using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 8f;

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }
}
