using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public int maxStackedItems = 10;
    public GameObject mainInventory, toolbar, equipInventory;
    private InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;


    int selectedSlot = -1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        int index = mainInventory.transform.childCount + toolbar.transform.childCount;

    inventorySlots = new InventorySlot[index];

    for (int i = 0; i < toolbar.transform.childCount; i++)
    {
        inventorySlots[i] = toolbar.transform.GetChild(i).GetComponent<InventorySlot>();
    }

    for (int i = 0; i < mainInventory.transform.childCount; i++)
    {
        inventorySlots[i + toolbar.transform.childCount] = mainInventory.transform.GetChild(i).GetComponent<InventorySlot>();
    }
    }

    private void Start()
    {
        ChangeSelectSlot(0);
    }

    private void Update() 
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number); 
            if (isNumber && number > 0 && number < 5)
            {
                ChangeSelectSlot(number - 1);
                if(inventorySlots[number - 1].GetComponentInChildren<InventoryItem>() != null && inventorySlots[number - 1].GetComponentInChildren<InventoryItem>().item != null)
                {
                    inventorySlots[number - 1].GetComponentInChildren<InventoryItem>().item.UseItem();
                }
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
            InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();
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
            InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();
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
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use) 
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem iteminSlot = slot.GetComponentInChildren<InventoryItem>();
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
            return item;
        }
        return null;
    }
}
