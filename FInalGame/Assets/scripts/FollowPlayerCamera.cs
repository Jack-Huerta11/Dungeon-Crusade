using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform target; // Reference to the player character's transform.
    public float smoothSpeed = 0.125f; // Adjust this to control camera smoothness.

    private Vector3 offset;

    private void Start()
    {
        // Calculate the initial offset between the camera and the player.
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Calculate the desired camera position.
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current camera position and the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position.
        transform.position = smoothedPosition;

        // Make the camera look at the player.
        transform.LookAt(target);
    }
}

