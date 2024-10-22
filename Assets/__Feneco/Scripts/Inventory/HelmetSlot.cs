using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelmetSlot : InventorySlot
{
    private int armor;
    private int bonusHP;

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<InventoryItem>().item.GetType() == typeof(Helmet))
        {
            if (transform.childCount == 0)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;

                SetStatus(inventoryItem.item as Helmet);
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
        Helmet helmet = item as Helmet;
        armor = helmet.armor;
        bonusHP = helmet.bonusHP;
    }

    public override void UpdatePlayerStatus(bool equip)
    {
        if (equip)
        {
            GameController.instance.armor += armor;
            GameController.instance.maxHealthPoints += bonusHP;
            GameController.instance.UpdateHUD();
            GameController.instance.UpdateEquipment(true, 0);
        }
        else
        {
            GameController.instance.armor -= armor;
            GameController.instance.maxHealthPoints -= bonusHP;
            GameController.instance.UpdateHUD();
            GameController.instance.UpdateEquipment(false, 0);
        }
    }
}
