using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;



[CreateAssetMenu(menuName = "Scriptable object/BodyArmor")]
public class BodyArmor : Item
{
    [SerializeField]
    public int armor = 0;
    public int hp = 0;
}
