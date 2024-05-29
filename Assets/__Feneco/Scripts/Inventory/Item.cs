using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;



[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
   
    [Header("Only gameplay")]
    //public ActionType actionType;

    [Header("Only UI")]
    public bool stackable;
    public bool consumable; 

    [Header("Both")]
    public Sprite image;

    public BodyArmor bodyArmor { get; private set; }

    public virtual void UseItem() { }
}
