using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private string _enemyName;

    [SerializeField] private int _maxHealthPoints;

    private int _healthPoints;

    [SerializeField] private int _damage;

    [SerializeField] private Sprite avatar;

    private Rigidbody rigidBody;

    public string EnemyName
    {
        get { return _enemyName; }
        set { _enemyName = value; }
    }
    public int HealthPoints
    {
        get { return _healthPoints; }
    }
    public int MaxHealthPoints
    {
        get { return _maxHealthPoints; }
    }
    public int Damage
    {
        get { return _damage; }
    }
    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _healthPoints = _maxHealthPoints;
    }
    public void TakeDamage(int damage)
    {
        _healthPoints -= damage;

        if(_healthPoints <= 0)
        {
            _healthPoints = 0;
        }

        if(_healthPoints == 0)
        {
            OnDeath();
        }
    }
    public void KnockBack(Vector3 direction, float knockbackForce)
    {
        direction.Normalize();

        rigidBody.AddForce(direction * knockbackForce, ForceMode.VelocityChange);
        StartCoroutine(KnockbackCooldown());
    }
    private IEnumerator KnockbackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        rigidBody.velocity = Vector3.zero;
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
