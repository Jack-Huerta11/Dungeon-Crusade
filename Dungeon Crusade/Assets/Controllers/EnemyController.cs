using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 2f;
    public float lookRadius = 10f;
	public ItemDropManager itemDropManager; // Reference to the item drop manager script
    public GameObject deathEffectPrefab;
    Transform target; 
    CharacterCombat combat;
    Animator animator;

	public int maxHealth = 100;
    private int currentHealth;
    void Start()
    {
		 currentHealth = maxHealth;
        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
        {
            target = PlayerManager.instance.player.transform;
            combat = GetComponent<CharacterCombat>();
            animator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("PlayerManager, player, or Animator is null in EnemyController.");
        }
    }
void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger entered: " + other.gameObject.name);

    // Assuming the player's attack collider has the "PlayerAttack" tag
    if (other.CompareTag("PlayerAttack"))
    {
        TakeDamage(100); // Adjust the damage value as needed
    }
}

	 void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Update()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(target.position, transform.position);

        Debug.Log("Distance to target: " + distance);

        if (distance <= lookRadius)
        {
            // Move towards the player
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            if (distance > stoppingDistance)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);

                // Attack when close enough
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                    animator.SetTrigger("attack");
                }
            }
        }
        else
        {
            // Stop moving if outside the lookRadius
            Debug.Log("Outside lookRadius.");
            animator.SetBool("isWalking", false);
        }
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
void Die()
    {
		animator.SetTrigger("die");
        // Instantiate death effect
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        // Drop a random item
        if (itemDropManager != null)
        {
            Item droppedItem = itemDropManager.DropRandomItem(transform.position);
            Debug.Log("Dropped item: " + droppedItem.itemName);
        }

        // Disable other components, like the collider, to prevent further interactions
        GetComponent<Collider>().enabled = false;

        // Disable the script or gameObject if needed
        // enabled = false;
        // gameObject.SetActive(false);
        Destroy(gameObject); // Destroy the enemy after death animation and item drop
    }
}

