using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public GameObject chestContents; // Reference to the contents of the chest, which might contain items or other objects.

    // List of food items that the player can receive.
    public List<FoodItem> foodItems = new List<FoodItem>();

    private bool hasBeenUsed = false; // Flag to track whether the chest has been used.

    public override void Interact()
    {
        if (hasBeenUsed)
        {
            // Chest has already been used; do nothing.
            return;
        }

        base.Interact(); // Call the base Interact method in the parent class if needed.

        if (foodItems.Count > 0)
        {
            int randomIndex = Random.Range(0, foodItems.Count);
            FoodItem randomFoodItem = foodItems[randomIndex];

            // Add the random food item to the inventory
            Inventory.instance.Add(randomFoodItem);

            // Display a message
            Debug.Log("You found " + randomFoodItem.foodName + " in the chest!");

            // Disable the chest
            DisableChest();
        }
    }

    private void DisableChest()
    {
        hasBeenUsed = true;
        // Optionally, disable the chest's renderer or game object here.
        if (chestContents != null)
        {
            chestContents.SetActive(false);
        }
    }
}
