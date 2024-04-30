using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private Vector3 impact;
    private float verticalVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
            
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    void UpdateSlopeSliding()
    {
        if(controller.isGrounded)
        {
            
        
        var sphereCastVerticalOffset = controller.height / 2 - controller.radius;
        var castOrigin = transform.position - new Vector3(0, sphereCastVerticalOffset, 0);

        if (Physics.SphereCast(castOrigin, controller.radius - .01f, Vector3.down,
            out var hit, .05f, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
        {
            var collider = hit.collider;
            var angle = Vector3.Angle(Vector3.up, hit.normal);

            if (angle > controller.slopeLimit)
            {
                var normal = hit.normal;
                var yInverse = 1f - normal.y;
                impact.x += yInverse * normal.x;
                impact.z += yInverse * normal.z;
            }
            
        }
        }
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

    internal void Reset()
    {
        verticalVelocity = 0f;
    }
}
