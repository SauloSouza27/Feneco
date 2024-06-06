using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;



[CreateAssetMenu(menuName = "Scriptable object/Potion")]
public class Potion : Item
{
    [SerializeField]
    private int healAmount = 0;

    public override void UseItem()
    {
        GameController.instance.Heal(healAmount);
        if(consumable)
        {
            InventoryManager.instance.UseSelectedItem(true);
        }
    }

}
