using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 6;
    [SerializeField] private float jump = 15, gravity = 1.02f,
    dashDuration = 0.3f, dashSpeed = 30f, dashCooldown = 1.5f, runModifier = 2.5f;

    [Header("Other Settings")]
    [SerializeField] private Transform cam;
    [SerializeField] private float timeScale = 1.0f;
    [SerializeField] private GameObject inventoryUI;

    private float speedModifier = 1.0f;
    private bool isExploded = false;
    private bool isDashing = false;
    private bool isCombat = false;
    private bool isRunning = false;
    [HideInInspector] public bool isInventory = false, isPaused = false;
    private float lastDashTime = -999f;
    private Vector2 move;
    private Vector3 movementDirection = Vector3.forward;
    private FreezeCameraRotation camFreeze;

    private Rigidbody rigidBody;
    [HideInInspector] public Animator animator;
    private GameObject talkingNPC;

    private int floorMask;
    private int enemiesMask;
    private int dunesMask;

    [HideInInspector] public float isCombatTimer = 0f;
    [HideInInspector] public bool isAttacking = false;
    public bool isNearNPC { get; private set; } = false;

    private const float camRayLength = 100f;
    private const float distanceToGround = 1.0f;
    
    public RectTransform inventoryUIRect; // Drag the inventory UI's RectTransform here in the Inspector
    public Vector2 hiddenPosition = new Vector2(10000, 10000); // Position to move the inventory off-screen
    public Vector2 visiblePosition = Vector2.zero; // Position to display the inventory

    

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        enemiesMask = LayerMask.GetMask("Enemies");
        dunesMask = LayerMask.GetMask("Dunes");
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Time.timeScale = timeScale;
        camFreeze = GameObject.Find("PlayerCamera").GetComponent<FreezeCameraRotation>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        //CombatChecker();
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return;

        RotateWithMovementDirection();
        UpdateMovementDirection();
        Movimento();
        animator.SetBool("IsJumping", !IsGrounded());
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && isNearNPC && talkingNPC != null)
        {
            FreezeCamera();
            talkingNPC.SendMessage("TalkNPC");
            talkingNPC.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        animator.SetBool("IsMoving", move.magnitude > 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (IsGrounded() && !isDashing && !isAttacking && !Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f, dunesMask))
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            rigidBody.velocity += Vector3.up * jump;
            animator.SetBool("IsJumping", true);
            SFXManager.instance.PlaySFX(SFXManager.SFXType.Jump);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (context.performed && Time.time >= lastDashTime + dashCooldown && !isAttacking && !Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f, dunesMask) && !isInventory && !isPaused)
        {
            isRunning = true;
            StartCoroutine(Dash());
            SFXManager.instance.PlaySFX(SFXManager.SFXType.Dash);
            lastDashTime = Time.time;
        }
        else if (context.canceled && isRunning)
        {
            isRunning = false;
            speedModifier -= runModifier;
            animator.SetBool("IsRunning", false);
        }
    }
    public void OnSettings(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bool isActive = MenuConfig.instance.gameObject.GetComponent<Canvas>().enabled;

            MenuConfig.instance.StartMenuConfig(!isActive);

            isPaused = !isActive;

            if (isActive)
            {   
                if (!isInventory)
                {
                    UnfreezeCamera();
                }
            }
            else if (!isActive)
            {
                FreezeCamera();
            }
        }
    }
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused && talkingNPC == null)
        {
            // Check if the inventory is currently visible
            bool isInventoryVisible = inventoryUIRect.anchoredPosition == visiblePosition;

            if (isInventoryVisible)
            {
                // Hide the inventory
                inventoryUIRect.anchoredPosition = hiddenPosition;
                isInventory = false;
                UnfreezeCamera();
            }
            else
            {
                // Show the inventory
                inventoryUIRect.anchoredPosition = visiblePosition;
                isInventory = true;
                SFXManager.instance.PlaySFX(SFXManager.SFXType.OpenInventory);
                FreezeCamera();
            }
        }
    }

    private IEnumerator Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            animator.SetBool("IsDashing", true);
            animator.SetBool("IsRunning", true);
            rigidBody.useGravity = false;

            Vector3 originalVelocity = rigidBody.velocity;
            Vector3 dashDirection = move.magnitude > 0 ? movementDirection : cam.forward;

            rigidBody.velocity = dashDirection * dashSpeed;
            yield return new WaitForSeconds(dashDuration);

            rigidBody.useGravity = true;
            rigidBody.velocity = originalVelocity;

            isDashing = false;
            animator.SetBool("IsDashing", false);
            speedModifier += runModifier;
        }
    }

    

    private void Movimento()
    {
        Vector3 cameraForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = move.y * cameraForward + move.x * cam.right;

        if (movement != Vector3.zero)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f, dunesMask))
            {
                movement = movement.normalized * (speed * speedModifier)/4.0f * Time.deltaTime;
                if (!isDashing && !isAttacking && !isInventory && !isPaused && !isExploded)
                {
                    rigidBody.velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
                }
            }
            else
            {
                movement = movement.normalized * (speed * speedModifier) * Time.deltaTime;
                if (!isDashing && !isAttacking && !isInventory && !isPaused && !isExploded)
                {
                    rigidBody.velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
                }
            }
        }

        rigidBody.velocity += Physics.gravity * gravity * Time.deltaTime;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.2f);
    }

    private void UpdateMovementDirection()
    {
        if (move.magnitude > 0)
        {
            Vector3 cameraForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = move.y * cameraForward + move.x * cam.right;
            movementDirection = movement.normalized;
        }
    }

    private void RotateWithMovementDirection()
    {
        if (move.magnitude > 0 && !isAttacking)
        {
            Quaternion newRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
    }

    public void SetNPCNearON(bool active, GameObject NPC)
    {
        isNearNPC = active;
        talkingNPC = active ? NPC : null;
    }

    private void CombatChecker()
    {
        isCombat = isCombatTimer > 0f;
        if (isCombat)
        {
            isCombatTimer -= Time.deltaTime;
        }
    }
    public void UnfreezeCamera()
    {
        camFreeze.UnfreezeRotation();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void FreezeCamera()
    {
        camFreeze.FreezeRotation();
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetIsPausedOFF()
    {
        isPaused = false;
    }

    internal IEnumerator DisableMovement(float v)
    {
        isExploded = true;
        yield return new WaitForSeconds(v);
        isExploded = false;
    }

    

}
