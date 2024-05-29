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

                Debug.Log("armadura entrou slot");

                SetStatus(inventoryItem.item.bodyArmor);
                UpdatePlayerStatus();

                Debug.Log("Armor: " + armor + "HP: "+bonusHP);
            }
        }
    }

    public override void SetStatus(BodyArmor bodyArmor)
    {
        armor = bodyArmor.armor;
        bonusHP = bodyArmor.bonusHP;
    }

    public override void UpdatePlayerStatus()
    {
        GameController.instance.armor = armor;
        GameController.instance.maxHealthPoints += bonusHP;
        GameController.instance.UpdateHUD();
    }
}
