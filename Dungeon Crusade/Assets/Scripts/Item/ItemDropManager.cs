
using UnityEngine;
 
public class ItemDropManager : MonoBehaviour
{
    public Item[] items; // Array of possible items to drop

    public Item DropRandomItem(Vector3 dropPosition)
    {
        if (items.Length == 0)
        {
            Debug.LogWarning("No items configured for item drop manager.");
            return null;
        }

        // Get a random item from the array
        Item randomItem = items[Random.Range(0, items.Length)];

        // Spawn the item at the drop position
        Instantiate(randomItem.itemPrefab, dropPosition, Quaternion.identity);

        return randomItem;
    }
}
