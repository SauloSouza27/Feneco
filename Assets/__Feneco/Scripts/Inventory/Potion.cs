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

    public override void useItem()
    {
        GameController.instance.Heal(healAmount);
        if(consumable)
        {
            InventoryManager.instance.GetSelectedItem(true);
        }
    }

}
