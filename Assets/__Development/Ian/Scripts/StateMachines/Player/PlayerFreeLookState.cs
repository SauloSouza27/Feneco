using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private Vector2 dodgingDirectionInput;
    private float remainingDodgeTime;
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
       stateMachine.InputReader.TargetEvent += OnTarget;
       stateMachine.InputReader.DodgeEvent += OnDodge;

       stateMachine.Animator.Play(FreeLookBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    private void OnTarget()
    {
        if(!stateMachine.Targeter.SelectTarget()) { return;}

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void OnDodge()
    {
        if(Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown) { return; }

        stateMachine.SetDodgeTime(Time.time);
        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
    }
    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 cameraForward = stateMachine.MainCameraTransform.forward;
        Vector3 cameraRight = stateMachine.MainCameraTransform.right;
 
        cameraForward.y = 0f;  
        
        cameraForward.Normalize();
 
        if (remainingDodgeTime > 0f)
        {
            
            Vector3 dodgeDirection = (cameraForward * dodgingDirectionInput.y + cameraRight * dodgingDirectionInput.x).normalized;
 
            
            Vector3 dodgeMovement = dodgeDirection * stateMachine.DodgeLenght / stateMachine.DodgeDuration;
 
        
            remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime, 0f);
 
            return dodgeMovement;
        }
        else
        {
            
            Vector3 movement = cameraForward * stateMachine.InputReader.MovementValue.y + cameraRight * stateMachine.InputReader.MovementValue.x;
 
            return movement;
        }
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, 
            Quaternion.LookRotation(movement), 
            deltaTime * stateMachine.RotationSmoothValue);
    }
}
