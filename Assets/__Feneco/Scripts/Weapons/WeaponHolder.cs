using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    public WeaponBase weapon;
    private float castTime;
    private float activeTime;
    private float cooldownTime;
    private Rigidbody rigidBody;
    private int dunesMask;
    private const float distanceToGround = 1.5f;
    [SerializeField] private Transform cam;

    private PlayerController playerController;
    private GameObject weaponHolding;

    enum AbilityState
    {
        ready,
        casttime,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;
    private void Start()
    {
        dunesMask = LayerMask.GetMask("Dunes");
        rigidBody = GetComponent<Rigidbody>();
        playerController = this.transform.GetComponent<PlayerController>();
        weaponHolding = GameObject.FindGameObjectWithTag("DanoArma");
        BoxCollider weaponCollider = weaponHolding.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
        Slash slashScript = weaponHolding.GetComponent<Slash>();
        slashScript.enabled = false;
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.casttime:
                if (castTime > 0)
                {
                    castTime -= Time.deltaTime;
                }
                else
                {
                    weapon.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = weapon.activeTime;
                    StartCoroutine(Attacking());
                    weaponHolding = GameObject.FindGameObjectWithTag("DanoArma");
                    BoxCollider weaponCollider = weaponHolding.GetComponent<BoxCollider>();
                    weaponCollider.enabled = true;
                    Slash slashScript = weaponHolding.GetComponent<Slash>();
                    slashScript.enabled = true;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    weapon.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = weapon.cooldownTime;
                    weaponHolding = GameObject.FindGameObjectWithTag("DanoArma");
                    BoxCollider weaponCollider = weaponHolding.GetComponent<BoxCollider>();
                    weaponCollider.enabled = false;
                    Slash slashScript = weaponHolding.GetComponent<Slash>();
                    slashScript.enabled = false;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (context.performed && state == AbilityState.ready && !playerController.isNearNPC && !playerController.isInventory && !playerController.isPaused && Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.15f) && !Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f, dunesMask))
        {
            state = AbilityState.casttime;
            weapon.CastTime(gameObject);
            castTime = weapon.castTime;
        }
    }
    private IEnumerator Attacking()
    {
        Vector3 attackRotation = new Vector3(cam.forward.x, 0, cam.forward.z);

        Quaternion newRotation = Quaternion.LookRotation(attackRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 9999f);


        Vector3 attackDirection = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;

        rigidBody.velocity = attackDirection * weapon.attackSpeed;
        yield return new WaitForSeconds(weapon.attackDuration);

        
    }
}