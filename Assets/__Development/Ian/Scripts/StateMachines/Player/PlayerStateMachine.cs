using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine_1
{
    [field: SerializeField] public InputReader InputReader {get; private set;}
    [field: SerializeField] public CharacterController Controller {get; private set;}
    [field: SerializeField] public Animator Animator {get; private set;}
    [field: SerializeField] public Targeter Targeter {get; private set;}
    [field: SerializeField] public ForceReceiver ForceReceiver {get; private set;}
    [field: SerializeField] public float FreeLookMovementSpeed {get; private set;}
    [field: SerializeField] public float TargetingMovementSpeed {get; private set;}
    [field: SerializeField] public float RotationSmoothValue {get; private set;}
    [field: SerializeField] public float DodgeDuration {get; private set;}
    [field: SerializeField] public float DodgeLenght {get; private set;}
    [field: SerializeField] public float DodgeCooldown {get; private set;}
    
    public float PreviousDodgeTime {get; private set;} = Mathf.NegativeInfinity; 
    public Transform MainCameraTransform {get; private set;}
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        
        SwitchState(new PlayerFreeLookState(this));
    }

    public void SetDodgeTime(float dodgeTime)
    {
        PreviousDodgeTime = dodgeTime;
    }

    
}
