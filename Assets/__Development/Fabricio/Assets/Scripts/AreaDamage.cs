using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public List<EnemyTeste> _enemiesInRange;

    public void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyTeste>();
        if (enemy != null)
        {
            _enemiesInRange.Add(enemy);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<EnemyTeste>();
        if (enemy != null && _enemiesInRange.Contains(enemy))
        {
            _enemiesInRange.Remove(enemy);
        }
    }

}

