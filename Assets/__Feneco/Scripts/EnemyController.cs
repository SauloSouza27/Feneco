using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public string enemyName;

    [SerializeField] public float HP;

    [SerializeField] private int damage;

    [SerializeField] private Sprite avatar;

    [SerializeField] private Mesh[] meshs;

    public int GetDamage()
    {
        return damage;
    }
    public void TakeDamage(float damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            HP = 0;
        }

        if(HP == 0)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        Destroy(gameObject);
    }
    public void SetMesh(MeshFilter meshFilter, int index)
    {
        meshFilter.mesh = meshs[index];
    }
}
