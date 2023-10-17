using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour
{
    // Health
    public int maxHealth = 100;
    public int currentHealth { get; protected set; }
    public Stat damage;
    public Stat armor;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        // Subtract the armor value
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Damage the character
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        healthBar.SetHealth(currentHealth);
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
