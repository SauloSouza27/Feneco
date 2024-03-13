using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
[SerializeField] private int damage;
[SerializeField] private AreaDamage areaDamage;

    private void OnAttack()
    {
        foreach (var attackAreaEnemies in areaDamage._enemiesInRange)
        {
            attackAreaEnemies.TakeDamage(damage);
        }
    }

}
