using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private Image image;
    public Color selectedColor, notSelectedColor;

    private void Start() 
    {
        image = transform.GetComponent<Image>();

        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    
    // Drag and drop
    public virtual void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) 
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;            
        }
    }
    public virtual void SetStatus(BodyArmor bodyArmor) { }

    public virtual void UpdatePlayerStatus() { }
}
