using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;



[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    //public ItemType type;
    //public ActionType actionType;

    [Header("Only UI")]
    public bool stackable;

    [Header("Both")]
    public Sprite image;

}
