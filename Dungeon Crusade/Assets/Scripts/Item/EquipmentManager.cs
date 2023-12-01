using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public enum MeshBlendShape { Torso, Arms, Legs };
    public Equipment[] defaultEquipment;

    public static EquipmentManager instance;
    public SkinnedMeshRenderer targetMesh;

    SkinnedMeshRenderer[] currentMeshes;

    void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;   // Items we currently have equipped

    // Callback for when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;    // Reference to our inventory

void Start()
{
    // Initialize currentEquipment based on number of equipment slots
    int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
    currentEquipment = new Equipment[numSlots];
    currentMeshes = new SkinnedMeshRenderer[numSlots];
	inventory = GetComponent<Inventory>();

    EquipDefaults();
}

    // Equip a new item
    public void Equip(Equipment newItem)
    {
        // Find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

        // An item has been equipped, so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;
        AttachToMesh(newItem, slotIndex);
    }

    // Unequip an item with a particular index
   public Equipment Unequip(int slotIndex)
{
      Equipment oldItem = null;
        // Only do this if an item is there
        if (currentEquipment[slotIndex] != null)
        {
            // Add the item to the inventory
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            SetBlendShapeWeight(oldItem, 0);
            // Destroy the mesh
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            // Remove the item from the equipment array
            currentEquipment[slotIndex] = null;

            // Equipment has been removed, so we trigger the callback
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
        return oldItem;
    }

    // Unequip all items
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaults();
    }

	void AttachToMesh(Equipment item, int slotIndex)
{
    if (targetMesh == null)
    {
        Debug.LogError("Target mesh is not assigned for equipment: " + item.name);
        return;
    }


    if (item.mesh != null && targetMesh != null)
    {
        SkinnedMeshRenderer newMesh = Instantiate(item.mesh) as SkinnedMeshRenderer;

        if (newMesh != null)
        {
            newMesh.transform.parent = targetMesh.transform.parent;

            newMesh.rootBone = targetMesh.rootBone;
            newMesh.bones = targetMesh.bones;

            currentMeshes[slotIndex] = newMesh;

            SetBlendShapeWeight(item, 100);
        }
        else
        {
            Debug.LogError("Failed to instantiate mesh for equipment: " + item.name);
        }
    }
    else
    {
        Debug.LogError("Mesh or targetMesh is not assigned for equipment: " + item.name);
    }
}

    void SetBlendShapeWeight(Equipment item, int weight)
    {
        foreach (MeshBlendShape blendshape in item.coveredMeshRegions)
        {
            int shapeIndex = (int)blendshape;
            targetMesh.SetBlendShapeWeight(shapeIndex, weight);
        }
    }

    void EquipDefaults()
    {
        foreach (Equipment e in defaultEquipment)
        {
            Equip(e);
        }
    }

    void Update()
    {
        // Unequip all items if we press U
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();


    }
}
