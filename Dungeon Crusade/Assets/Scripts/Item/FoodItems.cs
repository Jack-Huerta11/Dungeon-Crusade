using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FoodItem", menuName = "Inventory/FoodItem")]
public class FoodItem : Item
{
    public string foodName;
    public int healthRestoreAmount;

    public FoodItem(string name, int healthRestore)
    {
        foodName = name;
        healthRestoreAmount = healthRestore;
    }

    public override void Use()
    {
        base.Use();

        // Access the player's stats
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.RestoreHealth(healthRestoreAmount);

            // Log the action
            Debug.Log("Player ate " + foodName + " and restored " + healthRestoreAmount + " health.");

            // Remove the item from the player's inventory
            Inventory.instance.Remove(this);
        }
    }
}
