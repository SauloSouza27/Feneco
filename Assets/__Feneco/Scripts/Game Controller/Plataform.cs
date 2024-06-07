using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3[] waypoints;

    private int activeWaypoint = 0;

    private void Start()
    {
        waypoints = new Vector3[transform.childCount + 1];

        waypoints[0] = transform.position;

        for (int i = 1; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i-1).transform.position;
        }

        activeWaypoint = 1;
    }

    private void FixedUpdate()
    {
        if (waypoints.Length > 0)
        {
            transform.position = Vector3.Lerp(transform.position, waypoints[activeWaypoint], speed /1000.0f);

            float distance = Vector3.Distance(transform.position, waypoints[activeWaypoint]);

            if(distance < 5.0f)
            {
                activeWaypoint++;
                if(activeWaypoint == waypoints.Length)
                {
                    activeWaypoint = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.IsPlaying(this))
        {
            foreach(Transform t in transform)
            {
                Vector3 posTOffset = t.position;
                posTOffset = new Vector3(posTOffset.x, posTOffset.y + 1.5f, posTOffset.z);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(posTOffset, 0.5f);
            }
        }
    }
}
