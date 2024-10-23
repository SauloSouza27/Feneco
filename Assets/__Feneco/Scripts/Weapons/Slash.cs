using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public static Slash instance;

    public int damage { get; set; }
    [SerializeField] private float knockbackForce = 5.0f;
    [SerializeField] private GameObject impactVFX;
    [SerializeField] private Collider myCollider;

    private GameObject player;
    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevent processing the same collider multiple times
        if (alreadyCollidedWith.Contains(other)) 
        {
            return;
        }

        alreadyCollidedWith.Add(other);

        // Handle collision with an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            Vector3 direction = other.transform.position - player.transform.position;

            enemy.KnockBack(direction, knockbackForce);
            enemy.TakeDamage(damage);

            Instantiate(impactVFX, other.transform.position, player.transform.rotation);
        }

        // Handle collision with any object that has Health
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            Instantiate(impactVFX, other.transform.position, player.transform.rotation);
        }

        // Handle collision with any object that has ForceReceiver
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockbackForce);
        }
    }
}
