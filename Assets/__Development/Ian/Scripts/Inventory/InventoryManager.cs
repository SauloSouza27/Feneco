using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 10;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Start() {
        ChangeSelectSlot(0);
    }

    private void Update() 
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number); 
            if (isNumber && number > 0 && number < 8)
            {
                ChangeSelectSlot(number - 1);
            }
        }
    }

    void ChangeSelectSlot(int newValue) {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }


    public bool AddItem(Item item)
    {
        // Check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem1 iteminSlot = slot.GetComponentInChildren<InventoryItem1>();
            if (iteminSlot != null && 
                iteminSlot.item == item &&
                iteminSlot.count < maxStackedItems &&
                iteminSlot.item.stackable == true) 
            {
                iteminSlot.count++;
                iteminSlot.RefreshCount();
                return true;
            }
        }
        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem1 iteminSlot = slot.GetComponentInChildren<InventoryItem1>();
            if (iteminSlot == null) 
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot) 
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem1 inventoryItem = newItemGo.GetComponent<InventoryItem1>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use) 
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem1 iteminSlot = slot.GetComponentInChildren<InventoryItem1>();
        if (iteminSlot != null)
        {
            Item item = iteminSlot.item;
            if (use == true) 
            {
                iteminSlot.count--;
                if(iteminSlot.count <= 0)
                {
                    Destroy(iteminSlot.gameObject);  
                }
                else
                {
                    iteminSlot.RefreshCount();
                }
            }
        }
        return null;
    }
}
