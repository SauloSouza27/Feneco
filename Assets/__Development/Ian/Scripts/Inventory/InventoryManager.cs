using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public void AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();
            if (iteminSlot == null) 
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }

    void SpawnNewItem(Item item, InventorySlot slot) 
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem1 inventoryItem = newItemGo.GetComponent<InventoryItem1>();
        inventoryItem.InitialiseItem(item);
    }
}
