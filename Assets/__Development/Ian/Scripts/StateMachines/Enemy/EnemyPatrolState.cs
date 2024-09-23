using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private Vector3 walkpoint;
    private bool walkPointSet;
    public List<Vector3> waypointsList = new List<Vector3>();
    private Transform[] waypoints;
    
    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        walkpoint = stateMachine.Walkpoint;
        walkPointSet = stateMachine.WalkPointSet;

        if (stateMachine.EnemyWayPoints != null)
        {
            waypoints = new Transform[stateMachine.EnemyWayPoints.transform.childCount];

            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = stateMachine.EnemyWayPoints.transform.GetChild(i).transform;
            }
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        
        Patrolling(deltaTime);
        
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0.25f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        
    }

    private void Patrolling(float deltaTime)
    {
        
        if (!stateMachine.WalkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            MoveToPoint(deltaTime);
            Debug.Log("entering patrol22");
        }
        
        Vector3 distanceToWalkPoint = stateMachine.transform.position - walkpoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    
    private void MoveToPoint(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = walkpoint;
        
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    private void SearchWalkPoint()
    { 
        // float randomZ = Random.Range(-stateMachine.WalkPointRange, stateMachine.WalkPointRange);
        // float randomX = Random.Range(-stateMachine.WalkPointRange, stateMachine.WalkPointRange);
        //
        // walkpoint = new Vector3(stateMachine.transform.position.x + randomX,
        //     stateMachine.transform.position.y, stateMachine.transform.position.z + randomZ);
        //
        // Debug.Log(walkpoint);
        //
        // if (Physics.Raycast(walkpoint, -stateMachine.transform.up, 2f, stateMachine.whatIsGround))
        //     walkPointSet = true;
        
        

        // Randomly select an index from the list
        int randomIndex = Random.Range(0, waypoints.Count());

        // Return the randomly selected object
        walkpoint = waypointsList[randomIndex];
        walkPointSet = true;
    }
}
