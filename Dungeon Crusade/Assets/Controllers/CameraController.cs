using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The current target (player)
    public Transform lockOnTarget; // The target to lock onto
    public float smoothSpeed = 5.0f; // Smoothing factor for camera movement
    public Vector3 offset; // Offset of the camera relative to the target
    public float sensitivity = 2.0f; // Rotation sensitivity

    // Rotation limits for the camera
    public float minYAngle = -30.0f;
    public float maxYAngle = 80.0f;

    private float rotationX = 0;
    private float rotationY = 0;

    private bool isLockedOn = false; // Whether the camera is locked on

    private void LateUpdate()
    {
        if (!isLockedOn)
        {
            if (target == null)
            {
                Debug.LogWarning("Camera target is not set!");
                return;
            }

            // Calculate the desired position for the camera
            Vector3 desiredPosition = target.position - offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Implement camera rotation based on player input for free look
            RotateCamera();
        }
        else
        {
            if (lockOnTarget == null)
            {
                Debug.LogWarning("Lock-on target is not set!");
                isLockedOn = false;
                return;
            }

            // Calculate the desired position for the camera when locked on
            Vector3 desiredPosition = lockOnTarget.position - offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Always look at the lock-on target
            transform.LookAt(lockOnTarget);
        }
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the character horizontally based on the mouse X input
        target.Rotate(Vector3.up * mouseX * sensitivity);

        // Rotate the camera vertically (pitch) based on the mouse Y input
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);

        // Rotate the camera around its local X-axis (pitch)
        rotationY -= mouseY * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minYAngle, maxYAngle);

        transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
    }

    // Method to toggle lock-on mode
    public void ToggleLockOn(Transform newLockOnTarget = null)
    {
        isLockedOn = !isLockedOn;
        lockOnTarget = newLockOnTarget;
    }
}
