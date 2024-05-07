using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    
    // Drag and drop
    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
            InventoryItem1 inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem1>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
