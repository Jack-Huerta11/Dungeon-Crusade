using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private AudioSource audioSource; // Add this line

    private bool isWalking;
    private bool isAttacking;
    private bool isSprinting;

    public AudioClip attackSound; // Assign your attack sound in the Unity Editor

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        
        // Initialize AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = attackSound;
    }

    void Update()
    {
        UpdateMovementAnimation();
        UpdateAttackAnimation();
        UpdateSprintAnimation();
    }

    void UpdateMovementAnimation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        isWalking = (horizontalInput != 0f || verticalInput != 0f); 
        animator.SetBool("isWalking", isWalking);
    }

    void UpdateAttackAnimation()
    {
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", isAttacking);

            // Play attack sound
            if (attackSound != null)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isAttacking = false;
            animator.SetBool("isAttacking", isAttacking);
        }
    }

    void UpdateSprintAnimation()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("isRunning", isSprinting);
    }
}
