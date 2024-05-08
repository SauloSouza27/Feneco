using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = InventoryManager.instance.AddItem(itemsToPickup[id]);

        if (result)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("NO ITEM ADDED");
        }
    }

    public void GetSelectedItem() 
    {
        Item receivedItem = InventoryManager.instance.GetSelectedItem(false);
        if(receivedItem != null)
        {
            Debug.Log("Received item " + receivedItem);
        }
        else
        {
            Debug.Log("No item received!");
        }
    }

    public void UseSelectedItem() 
    {
        Item receivedItem = InventoryManager.instance.GetSelectedItem(true);
        if(receivedItem != null)
        {
            Debug.Log("Used item " + receivedItem);
        }
        else
        {
            Debug.Log("No item used!");
        }
    }
}
