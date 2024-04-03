using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public bool turnOnOff = true;

    private void OnDrawGizmos()
    {
        if (turnOnOff)
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.position, 0.5f);
        }
    }
}
