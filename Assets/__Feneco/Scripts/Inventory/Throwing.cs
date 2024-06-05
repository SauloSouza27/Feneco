using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public TextMeshProUGUI text_info;

    [Header("Stats")]
    public float throwCooldown, timeBetweenThrows;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;
    public float spread;
    public int throwsPerTap;

    int throwsToExecute;

    [Header("RayCasting")]
    public bool useRaycasts;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [Header("Extra Settings")]
    public bool allowButtonHold;
    bool throwing, readyToThrow, reloading;

    private void Start()
    {
        readyToThrow = true;
    }
    private void Update()
    {
        MyInput();

        // set info text
        //int grenadesLeft = InventoryManager.instance.GetGrenadeCount();
        //text_info.SetText("Grenades left: " + grenadesLeft);
    }
    private void MyInput()
    {
        if (allowButtonHold) throwing = Input.GetKey(KeyCode.Alpha1);
        else throwing = Input.GetKeyDown(KeyCode.Alpha1);

        // throw
        if (readyToThrow && throwing && !reloading && InventoryManager.instance.HasGrenades())
        {
            throwsToExecute = throwsPerTap;
            Throw();
        }
    }
    private void Throw()
    {
        readyToThrow = false;

        // spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // add force to your projectile (+ calculate direction)
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // direction with spread
        Vector3 forceDirection = cam.transform.forward + new Vector3(x, y, 0);

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized + new Vector3(x, y, 0);
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // use raycasts if needed
        if (useRaycasts)
        {
            if (Physics.Raycast(cam.transform.position, forceDirection, out rayHit, whatIsEnemy))
            {
                Debug.Log(rayHit.collider.name);
            }
        }

        // camera shake
        // camShake.Shake(camShakeDuration, camShakeMagnitude);

        InventoryManager.instance.UseGrenade();
        throwsToExecute--;

        // execute multiple throws per tap
        if (throwsToExecute > 0 && InventoryManager.instance.HasGrenades())
            Invoke(nameof(Throw), timeBetweenThrows);
        else if (throwsToExecute <= 0)
            Invoke(nameof(ResetThrow), throwCooldown);
    }
    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
