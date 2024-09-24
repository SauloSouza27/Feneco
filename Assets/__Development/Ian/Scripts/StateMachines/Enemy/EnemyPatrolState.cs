using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EnemyPatrolState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    
    private bool walkPointSet;
    private Vector3[] waypoints;
    private Vector3 startPosition;
    
    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }


    
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        startPosition = stateMachine.startPosition;
        //walkPointSet = false;
    }

    public override void Tick(float deltaTime)
    {
        //Move(deltaTime);
        
        
        
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        
        Patrolling(deltaTime);

        if (IsInStopRange())
        {
            walkPointSet = false;
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        
        
        stateMachine.Animator.SetFloat(SpeedHash, 0.5f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Agent.nextPosition = stateMachine.transform.position;
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void Patrolling(float deltaTime)
    {
        if (!walkPointSet) SearchWalkPoint();
        
        if (walkPointSet)
        {
            MoveToPoint(deltaTime);
            FacePoint();
        }
        
        // Vector3 distanceToWalkPoint = stateMachine.transform.position - stateMachine.Walkpoint;
        //
        // if (distanceToWalkPoint.magnitude < 1f)
        // {
        //     walkPointSet = false;
        // }
    }
    
    
    private void MoveToPoint(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.SetDestination(stateMachine.Walkpoint);
        
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
    
    protected void FacePoint()
    {
        Vector3 lookPos = stateMachine.Walkpoint - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);  
    }

    private void SearchWalkPoint()
    { 
        float randomZ = Random.Range(-stateMachine.WalkPointRange, stateMachine.WalkPointRange);
        float randomX = Random.Range(-stateMachine.WalkPointRange, stateMachine.WalkPointRange);
        
        stateMachine.Walkpoint = new Vector3(stateMachine.startPosition.x + randomX,
            stateMachine.startPosition.y, stateMachine.startPosition.z + randomZ);
        
        //Debug.Log(walkpoint);
        if (Physics.Raycast(stateMachine.Walkpoint, -stateMachine.transform.up, 2f, stateMachine.whatIsGround))
            walkPointSet = true;
    }
    
    private bool IsInStopRange()
    {
        float pointDistanceSqr = (stateMachine.Walkpoint - stateMachine.transform.position).sqrMagnitude;
        
        Debug.Log(pointDistanceSqr);

        return pointDistanceSqr <= 1f;
    }
    
   
}
