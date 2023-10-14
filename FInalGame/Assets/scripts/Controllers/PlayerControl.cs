using UnityEngine.EventSystems;
using UnityEngine;

/* Controls the player. Here we choose our "focus" and where to move. */

[RequireComponent(typeof(PlayerMotor))]
public class PlayerControl : MonoBehaviour {

    public Interactable focus;    // Our current focus: Item, Enemy etc.
    public LayerMask movementMask;  // Filter out everything not walkable
    public float movementSpeed = 5.0f;  // Movement speed

    Camera cam;            // Reference to our camera
    PlayerMotor motor;    // Reference to our motor

    // Get references
    void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }
    
    // Update is called once per frame
    void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // WASD controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        moveDirection.Normalize();

        if (moveDirection != Vector3.zero) {
            // Calculate movement vector in world space
            Vector3 movement = transform.TransformDirection(moveDirection) * movementSpeed;
            motor.Move(movement);
        }

        // If we press right mouse
        if (Input.GetMouseButtonDown(1)) {
            // We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    SetFocus(interactable);
                }
            }
        }
    }

    // ... Rest of the code remains the same

    // Set our focus to a new focus
    void SetFocus (Interactable newFocus) {
        // If our focus has changed
        if (newFocus != focus) {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;    // Set our new focus
            motor.FollowTarget(newFocus);    // Follow the new focus
        }
        newFocus.OnFocused(transform);
    }

    // Remove our current focus
    void RemoveFocus () {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
