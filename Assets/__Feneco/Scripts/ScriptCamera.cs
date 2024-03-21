using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    public Transform target; 
    public Vector2 angle; 
    public float distance = 5; 
    private float rotationSpeed = 500.0f;
    private float angleMinX;
    public float alturaCam;

    private void Start()
    {
        angleMinX = angle.x;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
            {
                angle.x += 1 * -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
                if (angle.x < angleMinX)
                {
                    angle.x = angleMinX;
                }
                if (angle.x > 54)
                {
                    angle.x = 54;
                }
                angle.y += 1 * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            }
        }
        Quaternion rotation = Quaternion.Euler(angle.x, angle.y, 0);
        Vector3 position = rotation * new Vector3(0, alturaCam, -distance) + target.position;

        transform.position = position;
        transform.rotation = rotation;
    }

}
