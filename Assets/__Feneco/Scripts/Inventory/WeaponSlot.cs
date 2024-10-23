using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : InventorySlot
{
    private int damage;
    public override void OnDrop(PointerEventData eventData) 
    {
        if(eventData.pointerDrag.GetComponent<InventoryItem>().item.GetType() == typeof(Weapon))
        {
            if (transform.childCount == 0) 
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;

                SetStatus(inventoryItem.item);
                UpdatePlayerStatus(true);
            }
        }        
    }
    
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
        {
            UpdatePlayerStatus(false);
        }
    }
    
    public override void SetStatus(Item item)
    {
        Weapon weapon = item as Weapon;
        damage = weapon.damage;
    }

    public void UpdatePlayerStatus(bool equipped)
    {
        if (equipped)
        {
            Slash.instance.damage += damage;
            GameController.instance.UpdateHudStatus(0);
            GameController.instance.UpdateEquipment(true, 2);
            
        }
        else
        {
            Slash.instance.damage -= damage;
            GameController.instance.UpdateHudStatus(0);
            GameController.instance.UpdateEquipment(false, 2);
        }
    }
}
