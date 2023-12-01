using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Camera playerCamera;
    public float walkSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float runMultiplier = 1.25f;
    public float rotationSpeed = 10f;
    public float maxJumpForce = 8f;
    public float gravity = 30f;
    private Interactable focus;
    private PlayerMotor motor;
    public PlayerStats PlayerStats { get; private set;}
    private Vector3 playerVelocity; 
    private bool isGrounded;

 void Update()
{
    HandleMovementInput();
    HandleJumpInput();

    // Check for object interaction using the "E" key
    if (Input.GetKeyDown(KeyCode.E))
    {
        TryInteractWithObject();
    }
}
  void Start()
    {
        // Assuming PlayerMotor is attached to the same GameObject
        motor = GetComponent<PlayerMotor>();
        // Other initialization code...
        PlayerStats = GetComponentInChildren<PlayerStats>();
    }
    // If we press right mouse
void TryInteractWithObject()
{
    Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, 100))
    {
        Interactable interactable = hit.collider.GetComponent<Interactable>();

        if (interactable != null)
        {
            float distance = Vector3.Distance(transform.position, hit.collider.transform.position);

            // Adjust the interactable distance based on your needs
            if (distance <= interactable.radius)
            {
                SetFocus(interactable);
            }
            else
            {
                RemoveFocus(); // If the object is out of range, remove focus
            }
        }
        else
        {
            RemoveFocus(); // If there's no interactable, remove focus
        }
    }
    else
    {
        RemoveFocus(); // If the ray doesn't hit anything, remove focus
    }
}

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 rotatedInput = playerCamera.transform.TransformDirection(inputDirection);
        rotatedInput.y = 0;

        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= Input.GetKey(KeyCode.LeftControl) ? runMultiplier : sprintMultiplier;
        }

        characterController.Move(rotatedInput * speed * Time.deltaTime);

        if (rotatedInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rotatedInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(maxJumpForce * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        isGrounded = characterController.isGrounded;
    }

 void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus; // Set our new focus

            // You need to initialize 'motor' with an instance of YourMotorClass.
            // For example, motor = new YourMotorClass();
            
            // Follow the new focus
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }
 // Remove our current focus
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;

        // You need to initialize 'motor' with an instance of YourMotorClass.
        // For example, motor = new YourMotorClass();
        
        motor.StopFollowingTarget();
    }
     public void ApplyUpgrade(float speedIncrease, float jumpIncrease, int healthIncrease, float attackSpeedIncrease)
    {
        walkSpeed += speedIncrease;
        maxJumpForce += jumpIncrease;

        // Optionally, you might want to put some constraints on the upgraded values
        // For example, ensure maxJumpForce doesn't exceed a maximum value.
        maxJumpForce = Mathf.Clamp(maxJumpForce, 0, maxJumpForce);
    }

}