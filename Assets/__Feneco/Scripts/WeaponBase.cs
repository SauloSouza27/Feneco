using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : ScriptableObject
{
    public new string name;
    [Header ("Weapon")]
    public float cooldownTime;
    public float activeTime;
    public float castTime;

    public virtual void CastTime(GameObject parent) { }
    public virtual void Activate(GameObject parent) { }
    public virtual void BeginCooldown(GameObject parent) { }
}