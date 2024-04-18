using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]

public class EnemyBase : ScriptableObject
{
    public new string name;

    [Header("Enemy")]
    public GameObject prefab;
    public float HP;
    public int damage;
    public Sprite avatar;
    public Mesh[] meshs;

    public virtual int GetDamage()
    {
        return damage;
    }
    public virtual void TakeDamage(int damage)
    {
        HP =- damage;
    }
    public virtual void SetMesh(MeshFilter meshFilter, int index)
    {
        meshFilter.mesh = meshs[index];
    }
}
