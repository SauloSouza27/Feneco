using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float velocidade = 15.0f, timeToDestroy = 0.5f;


    void Update()
    {
        MovimentoProjetil();
    }

        // Movimento bala
        void MovimentoProjetil()
    {
        transform.Translate(0, velocidade * Time.deltaTime, 0);
        Destroy(gameObject, timeToDestroy);
    }

}
