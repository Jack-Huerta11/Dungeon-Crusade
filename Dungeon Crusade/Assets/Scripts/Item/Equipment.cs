using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {



	public EquipmentSlot equipSlot;	// Slot to store equipment in

	public int armorModifier;		// Increase/decrease in armor
	public int damageModifier;      // Increase/decrease in damage
    public SkinnedMeshRenderer mesh;
    public EquipmentManager.MeshBlendShape[] coveredMeshRegions;

	// When pressed in inventory
	public override void Use()
{
    base.Use();

    if (EquipmentManager.instance != null)
    {
        EquipmentManager.instance.Equip(this); // Equip it

        if (Inventory.instance != null)
        {
            RemoveFromInventory(); // Remove it from inventory
        }
        else
        {
            Debug.LogError("Inventory.instance is not assigned.");
        }
    }
    else
    {
        Debug.LogError("EquipmentManager.instance is not assigned.");
    }
}

}
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }