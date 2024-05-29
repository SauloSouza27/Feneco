using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyArmorSlot : InventorySlot
{
    private BodyArmor bodyArmor = null;
    public int bodyArmorArmor = 0;

    //Definir armadura e retirar armadura quando desequipado utilizando o GameController.instance.UpdateArmor()? Fazer algo parecido com HP e dano?;
    //No momento, só armazenei o BodyArmor e o valor de armadura dele nas variáveis daqui, não fiz isso para os outros equipáveis

    public override void OnDrop(PointerEventData eventData) 
    {
        if(eventData.pointerDrag.GetComponent<InventoryItem>().item.GetType() == typeof(BodyArmor))
        {
            if (transform.childCount == 0) 
            {
                bodyArmor = eventData.pointerDrag.GetComponent<InventoryItem>().item as BodyArmor;
                bodyArmorArmor = bodyArmor.armor;
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
            }
        }        
    }
}
