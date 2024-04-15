using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponCreator : WeaponBase
{
    public GameObject pontaSaida, slash;

    public override void CastTime(GameObject parent)
    {
        PlayerController inCombat = parent.GetComponent<PlayerController>();
        inCombat.isCombat = true;
    }
    public override void Activate(GameObject parent)
    {
        pontaSaida = GameObject.FindGameObjectWithTag("Saida");
        Instantiate(slash, pontaSaida.transform.position, pontaSaida.transform.rotation);
    }
    public override void BeginCooldown(GameObject parent)
    {
        
    }
}
