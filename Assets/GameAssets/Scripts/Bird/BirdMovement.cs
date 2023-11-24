using UnityEngine;
using Lean.Touch;
public class BirdMovement : MonoBehaviour
{
    [Header("Bird Movement Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpRotationSpeed = 5f;
    [SerializeField] private float fallRotationSpeed = 5f;
    [SerializeField] private float jumpRotation = 45f;
    [SerializeField] private float fallRotation = -45f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;  
    private Quaternion targetRotation;
    private void OnEnable()
	{
		LeanTouch.OnFingerTap += HandleFingerTap;
	}
	private void OnDisable()
	{
		LeanTouch.OnFingerTap -= HandleFingerTap;
	}
    private void HandleFingerTap(LeanFinger finger)
	{
        rb.velocity = Vector2.up * jumpForce;
        EventManager.TriggerEvent(GameEvent.OnJump);
	}
    private void Update()
    {
        var rotationSpeed = rb.velocity.y >= 0 ? jumpRotationSpeed : fallRotationSpeed;
        targetRotation = rb.velocity.y >= 0 ? Quaternion.Euler(0, 0, jumpRotation) : Quaternion.Euler(0, 0, fallRotation);
        sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
