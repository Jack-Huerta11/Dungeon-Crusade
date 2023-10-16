using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float gravity = -9.81f;
    public float runSpeedMultiplier = 1.5f;
    public float crouchSpeedMultiplier = 0.5f;

    private CharacterController characterController;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;

        HandleMovement();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        ApplyGravity();
    }

private void HandleMovement()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

    float speed = moveSpeed;

    if (Input.GetKey(KeyCode.LeftShift)) // Running
    {
        speed *= runSpeedMultiplier;
    }

    if (Input.GetKey(KeyCode.C)) // Crouching
    {
        speed *= crouchSpeedMultiplier;
    }

    if (moveDirection != Vector3.zero)
    {
        // Rotate the player to face the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    characterController.Move(moveDirection * speed * Time.deltaTime);
}

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2.0f; // Ensure the character doesn't "float" on the ground
        }

        characterController.Move(velocity * Time.deltaTime);
    }
}
