using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the players stats and adds/removes modifiers when equipping items. */

public class PlayerStats : CharacterStats {

	// Use this for initialization
	void Start () {
		EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}
	
	// Called when an item gets equipped/unequipped
	void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
	{
		// Add new modifiers
		if (newItem != null)
		{
			armor.AddModifier(newItem.armorModifier);
			damage.AddModifier(newItem.damageModifier);
		}

		// Remove old modifiers
		if (oldItem != null)
		{
			armor.RemoveModifier(oldItem.armorModifier);
			damage.RemoveModifier(oldItem.damageModifier);
		}
		
	} 
	 public void RestoreHealth(int amount)
    {
        // Add logic here to restore the player's health.
        // You can ensure that health doesn't exceed the maximum health.

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update your health bar or perform other actions as needed.
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }


	public override void Die()
	{
		base.Die();
		PlayerManager.instance.KillPlayer();
	}
}