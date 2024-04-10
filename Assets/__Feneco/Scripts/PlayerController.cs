using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 6, jump = 8, gravity = 1.02f, dashDuration = 0.3f, dashSpeed = 30f, dashCooldown = 1.5f;
    private bool isDashing = false;
    private bool isCombat = false;
    int floorMask;
    float camRayLength = 100f;
    private Vector2 move;
    private Rigidbody rb;
    public Transform cam;
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
            rb.velocity += Vector3.up * jump;
            animator.SetBool("IsJumping", true);
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(Dash());
            lastDashTime = Time.time;
        }
    }
    private IEnumerator Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            animator.SetBool("IsDashing", true); // Set IsDashing to true when dashing
            rb.useGravity = false;

            Vector3 originalVelocity = rb.velocity;

            Vector3 dashDirection = move.magnitude > 0 ? movementDirection : cam.forward;

            rb.velocity = dashDirection * dashSpeed;

            yield return new WaitForSeconds(dashDuration);

            rb.useGravity = true;
            rb.velocity = originalVelocity;

            isDashing = false;
            animator.SetBool("IsDashing", false); // Set IsDashing to false when dash ends
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
            movement = movement.normalized * speed * Time.deltaTime;

            rb.MovePosition(transform.position + movement);
        }
        rb.velocity += Physics.gravity * gravity * Time.deltaTime;
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float distanceToGround = 1.0f;
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