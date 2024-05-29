using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyArmorSlot : InventorySlot
{
    private int armor;
    private int bonusHP;

    //Definir armadura e retirar armadura quando desequipado utilizando o GameController.instance.UpdateArmor()? Fazer algo parecido com HP e dano?;
    //No momento, só armazenei o BodyArmor e o valor de armadura dele nas variáveis daqui, não fiz isso para os outros equipáveis

    public override void OnDrop(PointerEventData eventData) 
    {
        if(eventData.pointerDrag.GetComponent<InventoryItem>().item.GetType() == typeof(BodyArmor))
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
        BodyArmor bodyArmor = item as BodyArmor;
        armor = bodyArmor.armor;
        bonusHP = bodyArmor.bonusHP;
    }

    public override void UpdatePlayerStatus(bool equip)
    {
        if (equip)
        {
            GameController.instance.armor += armor;
            GameController.instance.maxHealthPoints += bonusHP;
            GameController.instance.UpdateHUD();
        }
        else
        {
            GameController.instance.armor -= armor;
            GameController.instance.maxHealthPoints -= bonusHP;
            GameController.instance.UpdateHUD();
        }
    }
}
