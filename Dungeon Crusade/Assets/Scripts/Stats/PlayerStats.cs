using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Handles the players stats and adds/removes modifiers when equipping items. */

public class PlayerStats : CharacterStats
{
    public HealthBar healthBar;
    private int points = 0;
    public int Points
    {
        get { return points; }
        set { points = value; }
    }
    public Text pointsText; // Reference to the UI text displaying points

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        // Initialize currentHealth with maxHealth on start
        currentHealth = maxHealth;
    }
    // Called when an item gets equipped/unequipped
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
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
     void Update()
    {
        // For testing purposes, increase points when the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreasePoints(1);
        }
    }
    public void RestoreHealth(int amount)
    {
        ModifyHealth(amount);

        if (currentHealth > maxHealth) 
        {
            SetHealth(maxHealth);
        }

        // Update your health bar or perform other actions as needed.
        if (healthBar != null)
        {
            healthBar.SetHealth((int)currentHealth);
        }
    }
 public override void Die()
    {
        base.Die();
        IncreasePoints(1); // Increase points by 1 when an enemy dies
        PlayerManager.instance?.KillPlayer(); // Null check for PlayerManager.instance
    }

    public void IncreasePoints(int amount)
    {
        points += amount;
        UpdatePointsUI(); // Update the UI when points are increased
    }

    public void UpdatePointsUI()
    {
        // Implement the logic to update your UI with the current points
        // Example: Update a text component on your UI
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString();
        }
    }
}