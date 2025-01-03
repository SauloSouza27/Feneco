using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public bool destroyOnHit;

    [Header("Effects")]
    public GameObject muzzleEffect;
    public GameObject hitEffect;

    [Header("Explosive Projectile")]
    public bool isExplosive;
    public float explosionRadius;
    public float explosionForce;
    public float upwardsExplosionForce;
    public int explosionDamage;
    public GameObject explosionEffect;
    public float explosionDelay; // Add this line

    private bool isTimeToExplode = false;

    private Rigidbody rb;
    private bool hitTarget;

    private void Start()
    {
        // get rigidbody component
        rb = GetComponent<Rigidbody>();

        // spawn muzzleEffect (if assigned)
        if (muzzleEffect != null)
            Instantiate(muzzleEffect, transform.position, Quaternion.identity);

        // Start explosion timer if projectile is explosive
        if (isExplosive)
        {
            StartCoroutine(DelayedExplosion());
        }
    }

    private void FixedUpdate()
    {
        if (isTimeToExplode)
        {
            Explode();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hitTarget)
            return;
        else
            hitTarget = true;

        // enemy hit
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

            // deal damage to the enemy
            enemy.TakeDamage(damage);

            // spawn hit effect (if assigned)
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            // destroy projectile
            if (!isExplosive && destroyOnHit)
                Invoke(nameof(DestroyProjectile), 0.1f);
        }

        // make sure projectile sticks to surface if not explosive
        if (!isExplosive)
        {
            rb.isKinematic = true;
            transform.SetParent(collision.transform);
        }
    }

    private IEnumerator DelayedExplosion()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(explosionDelay);
        isTimeToExplode = true;
    }
        

    private void Explode()
    {
    // spawn explosion effect (if assigned)
    if (explosionEffect != null)
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

    // find all the objects that are inside the explosion range
    Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

    // loop through all of the found objects and apply damage and explosion force
    foreach (Collider nearbyObject in objectsInRange)
    {
        if (nearbyObject.gameObject == gameObject)
            {
                continue;
            }

        // check if object is enemy, if so deal explosionDamage
        if (nearbyObject.GetComponent<EnemyController>() != null)
            {
                Debug.Log("damage: "+ explosionDamage);
                nearbyObject.GetComponent<EnemyController>().TakeDamage(explosionDamage);
                nearbyObject.GetComponent<EnemyController>().PauseNavMeshAgent(1.5f);  // Disable NavMeshAgent for 0.5 seconds
            }

        // check if object has a rigidbody
        Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
        if (rb != null)
            {
                if (nearbyObject.CompareTag("Player"))
                {
                    nearbyObject.GetComponent<PlayerController>().StartCoroutine(nearbyObject.GetComponent<PlayerController>().DisableMovement(1f));
                }
                // apply explosion force
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsExplosionForce, ForceMode.Impulse);
                
                Debug.Log("Kabooom " + nearbyObject.name);
                StartCoroutine(ExplosionEnd(rb.velocity));
            }
    }
        // destroy projectile with 0.1 seconds delay
        Invoke(nameof(DestroyProjectile), 0.1f);

        isTimeToExplode = false;
    }   

    private IEnumerator ExplosionEnd(Vector3 velocity)
    {
        yield return new WaitForSeconds(0.5f);
        velocity = Vector3.zero;
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    // just graphics stuff
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
