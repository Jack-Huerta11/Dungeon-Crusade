using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    private bool isGrounded = false; // Initially, the player is not grounded

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // The player is no longer grounded after jumping
        }

        // Grounded detection logic (Raycasting example)
        Ray groundRay = new Ray(transform.position, Vector3.down);
        float maxRayDistance = 0.1f; // Adjust this based on your character's size
        if (Physics.Raycast(groundRay, maxRayDistance))
        {
            isGrounded = true; // The player is grounded if the ray hits something below
        }
    }
}
