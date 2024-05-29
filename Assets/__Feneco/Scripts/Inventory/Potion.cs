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
    private bool consumable = true;

    public override void useItem()
    {
        GameController.instance.Heal(healAmount);
        if(consumable)
        {
            //deletar o item (ou uma quantidade dele) quando for utilizado
        }
    }

}
