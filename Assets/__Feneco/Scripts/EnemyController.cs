using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float HP;

    [SerializeField] private int damage;

    [SerializeField] private Sprite avatar;

    [SerializeField] private Mesh[] meshs;

    public int GetDamage()
    {
        return damage;
    }
    public void TakeDamage(int damage)
    {
        HP = -damage;
    }
    public void SetMesh(MeshFilter meshFilter, int index)
    {
        meshFilter.mesh = meshs[index];
    }
}
