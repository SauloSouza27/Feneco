using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    [SerializeField] private float knockbackForce = 5.0f;
    [SerializeField] private GameObject impactVFX;
    //    private float timeToDestroy = 0.5f;
    private GameObject player;
    
    [SerializeField] private Collider myCollider;
    
    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    
    void Start()
    {
//        Destroy(gameObject, timeToDestroy);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
//        Transform saida = player.transform.GetChild(2);
//        transform.position = saida.position;
//        transform.rotation = saida.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            Vector3 direction = other.transform.position - player.transform.position;

            enemy.KnockBack(direction, knockbackForce);
            enemy.TakeDamage(damage);

            Instantiate(impactVFX, other.transform.position, player.transform.rotation);
        }
        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);
        
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            Instantiate(impactVFX, other.transform.position, player.transform.rotation);
        }
        
        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockbackForce);
        }
        
        
    }
}