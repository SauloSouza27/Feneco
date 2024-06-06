using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public string enemyName { get; set; }

    [SerializeField] public int maxHealthPoints { get; private set; }

    public int healthPoints { get; private set; }

    [SerializeField] public int damage { get; private set; }

    [SerializeField] private Sprite avatar;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        healthPoints = maxHealthPoints;
    }
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;

        if(healthPoints <= 0)
        {
            healthPoints = 0;
        }

        if(healthPoints == 0)
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
