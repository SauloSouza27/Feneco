using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelmetSlot : InventorySlot
{
    public override void OnDrop(PointerEventData eventData) 
    {
        if(eventData.pointerDrag.GetComponent<InventoryItem>().item.GetType() == typeof(Helmet))
        {
            if (transform.childCount == 0) 
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
            }
        }        
    }
}
