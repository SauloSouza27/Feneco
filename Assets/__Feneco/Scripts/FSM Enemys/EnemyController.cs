using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public string enemyName { get; set; }

    [SerializeField] public int maxHealthPoints { get; private set; }

    public int healthPoints { get; private set; }

    [SerializeField] private int enemyDamage;

    public int damage { get; private set; }

    [SerializeField] private Sprite avatar;

    private Rigidbody rigidBody;

    private bool isExploded = false;

    private NavMeshAgent navMeshAgent;
    private Vector3 savedPosition;

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        healthPoints = maxHealthPoints;
        damage = enemyDamage;
        navMeshAgent = GetComponent<NavMeshAgent>();
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


    public void PauseNavMeshAgent(float duration)
    {
        if (!isExploded)
        {
            StartCoroutine(PauseNavMeshAgentCoroutine(duration));
        }
    }
    

    private IEnumerator PauseNavMeshAgentCoroutine(float duration)
    {
        isExploded = true;

        navMeshAgent.isStopped = true;
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        // Save the current position and reset velocity to zero
        //savedPosition = transform.position;
        //navMeshAgent.velocity = Vector3.zero;

        yield return new WaitForSeconds(duration);

        // Ensure the agent remains at the saved position
        //navMeshAgent.Warp(savedPosition);

        navMeshAgent.isStopped = false;
        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = true;

        isExploded = false;
    }
}
