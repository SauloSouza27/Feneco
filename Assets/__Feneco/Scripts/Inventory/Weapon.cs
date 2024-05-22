using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;



[CreateAssetMenu(menuName = "Scriptable object/Weapon")]
public class Weapon : Item
{
    [SerializeField]
    public int damage = 0;
}
