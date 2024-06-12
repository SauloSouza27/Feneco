using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Granade")]
public class Granade : Item
{
    public override void UseItem()
    {
        Throwing.instance.usedInventoryItem = true;
    }
}
