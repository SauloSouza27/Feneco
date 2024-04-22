using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private string _enemyName;

    [SerializeField] private int HP;

    [SerializeField] private int maxHP;

    [SerializeField] private int damage;

    [SerializeField] private Sprite avatar;

    [SerializeField] private Mesh[] meshs;

    private Rigidbody rigidBody;

    public string EnemyName
    {
        get { return _enemyName; }
        set { _enemyName = value; }
    }

    private void Start()
    {
        HP = maxHP;
    }
    public int GetDamage()
    {
        return damage;
    }
    public void TakeDamage(int damage)
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
    public void KnockBack(Vector3 direction, float knockbackForce)
    {
        direction.Normalize();

        rigidBody.AddForce(direction * knockbackForce, ForceMode.VelocityChange);
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
    public void SetMesh(MeshFilter meshFilter, int index)
    {
        meshFilter.mesh = meshs[index];
    }
    public int GetEnemyHP()
    {
        return HP;
    }
    public int GetEnemyMaxHP()
    {
        return maxHP;
    }
}
