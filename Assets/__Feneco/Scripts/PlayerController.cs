using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 6, jump = 15, gravity = 1.02f, dashDuration = 0.3f, dashSpeed = 30f, dashCooldown = 1.5f, runModifier = 2.5f;
    private float speedModifier = 1.0f;
    private bool isDashing = false;
    private bool isCombat = false;
    private bool isNearNPC = false;
    private bool isRunning = false;
    private GameObject talkingNPC;
    int floorMask;
    float camRayLength = 100f;
    private Vector2 move;
    private Rigidbody rb;
    [SerializeField] private Transform cam;
    private Vector3 movementDirection;
    private float lastDashTime = -999f;
    private Animator animator;
    [SerializeField] private float timeScale = 1.0f;

    
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Time.timeScale = timeScale;
    }

    void FixedUpdate()
    {
        if (isCombat)
        {
            PlayerOlhandoProMouse();
        }
        else
        {
            RotateWithMovementDirection();
        }

        UpdateMovementDirection();
        Movimento();
        animator.SetBool("IsJumping", !IsGrounded());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        if (move.magnitude == 0)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && isDashing == false)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.velocity += Vector3.up * jump;
            animator.SetBool("IsJumping", true);
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= lastDashTime + dashCooldown)
        {
            isRunning = true;
            StartCoroutine(Dash());
            lastDashTime = Time.time;
            speedModifier += runModifier;
        }
        else if (context.canceled && isRunning == true)
        {
            isRunning = false;
            speedModifier -= runModifier;
            animator.SetBool("IsRunning", false);
        }
    }
    private IEnumerator Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            animator.SetBool("IsDashing", true);
            animator.SetBool("IsRunning", true);
            rb.useGravity = false;

            Vector3 originalVelocity = rb.velocity;

            Vector3 dashDirection = move.magnitude > 0 ? movementDirection : cam.forward;

            rb.velocity = dashDirection * dashSpeed;

            yield return new WaitForSeconds(dashDuration);

            rb.useGravity = true;
            rb.velocity = originalVelocity;

            isDashing = false;
            animator.SetBool("IsDashing", false);
        }
    }
    public void OnAction(InputAction.CallbackContext context)
    {
        // Talk NPC
        if (context.performed && isNearNPC && talkingNPC != null)
        {
            talkingNPC.SendMessage("TalkNPC");
            talkingNPC.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void SetNPCNearON(bool active, GameObject NPC)
    {
        if (active)
        {
            isNearNPC = active;
    
            if (NPC != null)
            {
                talkingNPC = NPC;
            }
        }
        else
        {
            isNearNPC = active;
            talkingNPC = null;
        }
    }
    private void RotateWithMovementDirection()
    {
        if (move.magnitude > 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
    }

    private void PlayerOlhandoProMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = newRotation;
        }
    }

    private void Movimento()
    {
        Vector3 cameraForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 movement = move.y * cameraForward + move.x * cam.right;

        if (movement != Vector3.zero)
        {
            movement = movement.normalized * speed * speedModifier * Time.deltaTime;

            rb.MovePosition(transform.position + movement);
        }
        rb.velocity += Physics.gravity * gravity * Time.deltaTime;
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float distanceToGround = 1.5f;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distanceToGround + 0.1f))
        {
            return true;
        }
        return false;
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
}