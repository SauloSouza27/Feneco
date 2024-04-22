using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    [SerializeField] private float knockbackForce = 2.0f;
    private float timeToDestroy = 0.5f;
    private GameObject player;

    
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Transform saida = player.transform.GetChild(2);
        transform.position = saida.position;
        transform.rotation = saida.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            Vector3 direction = other.transform.position - player.transform.position;

            enemy.TakeDamage(damage);
            enemy.KnockBack(direction, knockbackForce);
        }
    }
}