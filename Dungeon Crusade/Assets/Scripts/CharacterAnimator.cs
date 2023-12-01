using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float locomationAnimationSmoothTime = .1f;
    bool isWalking;
    bool isAttacking;
    bool isRunning;
    NavMeshAgent agent;
    protected Animator animator; 
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

	protected virtual void Start () {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;

	}
	
 protected virtual void Update()
{
    // Check for movement input
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");
    isWalking = (horizontalInput != 0f || verticalInput != 0f);
    animator.SetBool("isWalking", isWalking);

    // Check for attack input
    bool isAttacking = Input.GetMouseButtonDown(1); // Assuming right mouse click is for attack
    animator.SetBool("isAttacking", isAttacking);

    // Check for sprint input
    bool isSprinting = Input.GetKey(KeyCode.LeftShift);
    animator.SetBool("isRunning", isRunning);
}
    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
