using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Grenade")]
public class Grenade : Item
{
    public override void UseItem()
    {
        Throwing.instance.usedInventoryItem = true;
        if(consumable)
        {
            InventoryManager.instance.UseSelectedItem(true);
        }
    }
}
