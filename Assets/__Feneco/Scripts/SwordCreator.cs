using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwordCreator : WeaponBase
{
    public GameObject pontaSaida, slash1, slash2;
    private bool attack = true;

    public override void CastTime(GameObject parent)
    {
        PlayerController inCombat = parent.GetComponent<PlayerController>();
        inCombat.isCombatTimer = 3.0f;
        inCombat.isAttacking = true;
    }
    public override void Activate(GameObject parent)
    {
        pontaSaida = GameObject.FindGameObjectWithTag("Saida");
        PlayerController animator = parent.GetComponent<PlayerController>();
        if(attack == true)
        {
            animator.animator.SetBool("Attack1", true);
            Instantiate(slash1, pontaSaida.transform.position, pontaSaida.transform.rotation);
            Debug.Log("Attack1");
            attack = false;
        }
        else
        {
            animator.animator.SetBool("Attack2", true);
            Instantiate(slash2, pontaSaida.transform.position, pontaSaida.transform.rotation);
            Debug.Log("Attack2");
            attack = true;
        }
        
    }
    public override void BeginCooldown(GameObject parent)
    {
        PlayerController inCombat = parent.GetComponent<PlayerController>();
        inCombat.isAttacking = false;
        if(attack == false)
        {
            inCombat.animator.SetBool("Attack1", false);
        }
        else
        {
            inCombat.animator.SetBool("Attack2", false);
        }
    }
}
