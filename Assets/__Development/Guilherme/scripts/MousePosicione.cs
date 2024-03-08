using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MousePosicione : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform player;
    public float maxRadius;
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
        {
            Vector3 mousePos = raycastHit.point;

            Vector3 difference = mousePos - player.position;
            float magnitude = difference.magnitude;
            if (magnitude > maxRadius)
            {
                difference = difference * (maxRadius / magnitude);
            }
            transform.position = player.position + difference;
        }
    }
}