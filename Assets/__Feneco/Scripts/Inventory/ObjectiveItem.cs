using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable object/ObjectiveItem")]
public class ObjectiveItem : Item
{
    public void DeliverItemNPC()
    {
        InventoryManager.instance.DeliverItemToNPC();
    }
}
