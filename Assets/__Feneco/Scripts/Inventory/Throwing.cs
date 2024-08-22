using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Throwing : MonoBehaviour
{
    public static Throwing instance;

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
    private bool throwing, readyToThrow, reloading;

    public bool usedInventoryItem { get; set; }

    private void Awake()
    {
        instance = transform.GetComponent<Throwing>();
    }
    private void Start()
    {
        readyToThrow = true;
    }
    private void FixedUpdate()
    {
        if (readyToThrow && !reloading  && usedInventoryItem)
        {
            throwsToExecute = throwsPerTap;
            Throw();
        }

        // set info text
        //int grenadesLeft = InventoryManager.instance.GetGrenadeCount();
        //text_info.SetText("Grenades left: " + grenadesLeft);
    }

    private void Throw()
    {
        readyToThrow = false;
        usedInventoryItem = false;

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

        //InventoryManager.instance.UseGrenade();
        throwsToExecute--;

        // execute multiple throws per tap
        //if (throwsToExecute > 0 && InventoryManager.instance.HasGrenades())
        //    Invoke(nameof(Throw), timeBetweenThrows);
        if (throwsToExecute <= 0)
            Invoke(nameof(ResetThrow), throwCooldown);
    }
    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
