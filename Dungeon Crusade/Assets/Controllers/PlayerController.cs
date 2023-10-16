using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CameraController cameraController;
    private Transform currentLockOnTarget;
    private float lockOnRadius = 10f; // Adjust this radius as needed

    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObject();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleLockOn();
        }
    }

    void InteractWithObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f); // Change the radius as needed

        float nearestDistance = Mathf.Infinity;
        Transform nearestInteractable = null;

        foreach (var collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = collider.transform;
                }
            }
        }

        if (nearestInteractable != null)
        {
            nearestInteractable.GetComponent<Interactable>().Interact();
        }
    }

    void ToggleLockOn()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, lockOnRadius);

        float nearestDistance = Mathf.Infinity;
        Transform nearestInteractable = null;

        foreach (var collider in interactables)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = collider.transform;
                }
            }
        }

        if (nearestInteractable != null)
        {
            if (currentLockOnTarget != null)
            {
                // Unlock from the current lock-on target
                cameraController.ToggleLockOn(null);
            }

            // Lock on to the nearest interactable
            cameraController.ToggleLockOn(nearestInteractable);
            currentLockOnTarget = nearestInteractable;
        }
        else
        {
            // Unlock from the current lock-on target
            cameraController.ToggleLockOn(null);
            currentLockOnTarget = null;
        }
    }
}
