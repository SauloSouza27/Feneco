using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera1 : MonoBehaviour
{
    public Transform target; //Purpose of the object
    public Vector2 angle; //Purpose of the object
    public float distance = 5; //Distance
    private float rotationSpeed = 0.001f;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            CamOrbit();
        }
    }
    private void CamOrbit()
    {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, -verticalInput);
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }
}
