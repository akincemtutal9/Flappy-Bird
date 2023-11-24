
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpRotationSpeed = 5f;
    [SerializeField] private float fallRotationSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;

    private Quaternion targetRotation;

    private void Update()
    {
        var rotationSpeed = rb.velocity.y >= 0 ? jumpRotationSpeed : fallRotationSpeed;
        targetRotation = rb.velocity.y >= 0 ? Quaternion.Euler(0, 0, 45) : Quaternion.Euler(0, 0, -45);
        if (Input.GetKeyDown(KeyCode.Space)) {rb.velocity = Vector2.up * jumpForce;};
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
