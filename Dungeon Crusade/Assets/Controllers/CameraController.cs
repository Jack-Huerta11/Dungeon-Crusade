using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float sensitivity = 2f; // Mouse sensitivity 
    public float rotationSmoothing = 0.1f; // Smoothing factor for camera rotation
    public float distanceFromPlayer = 5f; // Distance from the player
    public float heightOffset = 2f; // Height offset from the player

    private float mouseX, mouseY;

    void Update()
    {
        HandleRotationInput();
    }

    void HandleRotationInput()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Clamp vertical rotation to prevent camera flipping
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // Smoothly interpolate rotation
        Vector3 targetRotation = new Vector3(mouseY, mouseX);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRotation, rotationSmoothing);

        // Calculate the new position based on player's position and camera orientation
        Vector3 offset = Quaternion.Euler(mouseY, mouseX, 0) * new Vector3(0, 0, -distanceFromPlayer);
        Vector3 newPosition = playerTransform.position + offset + Vector3.up * heightOffset;

        // Move the camera to the new position
        transform.position = Vector3.Lerp(transform.position, newPosition, rotationSmoothing);
        
        // Ensure the camera always looks at the player
        transform.LookAt(playerTransform);
    }
}
