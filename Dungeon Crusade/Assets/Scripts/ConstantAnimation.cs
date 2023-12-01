using UnityEngine;

public class ConstantAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        // Check if the Animator component is not null and has an Animator Controller
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            // Play the default animation (first animation in the Animator Controller)
            animator.Play("YourConstantAnimationClipName");
        } 
        else
        {
            Debug.LogError("Animator component or Animator Controller is missing!");
        }
    }
}
