using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private int hp;

    public void TakeDamage(int damageValue)
    {
        hp -= damageValue;
    }
}
