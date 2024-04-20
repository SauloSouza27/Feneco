using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private float damage = 2.0f;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
        }
    }
}