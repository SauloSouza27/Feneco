using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : InventorySlot
{
    [SerializeField] private GameObject snapHand, snapBack;
    [SerializeField] private GameObject weapon;
    public override void OnDrop(PointerEventData eventData) 
    {
        if(eventData.pointerDrag.GetComponent<InventoryItem>().item.GetType() == typeof(Weapon))
        {
            if (transform.childCount == 0) 
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;

                DrawWeapon(true, weapon);
            }
        }        
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
        {
            DrawWeapon(false, weapon);
        }
    }

    public void DrawWeapon(bool draw, GameObject weapon)
    {
        if (draw)
        {
            weapon.transform.SetParent(snapHand.transform);
            weapon.transform.SetLocalPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            weapon.transform.SetParent(snapBack.transform);
            weapon.transform.SetLocalPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));
        }
    }
}
