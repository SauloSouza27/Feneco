using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Patrol : StateMachineBehaviour
{
    public string waypointAreaName;

    private GameObject player = null;

    private GameObject waypointArea = null;

    private Transform[] waypoints;

    private int choosenWaypoint = 0;

    private NavMeshAgent navMeshAgent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");

        navMeshAgent = animator.transform.GetComponent<NavMeshAgent>();

        waypointArea = GameObject.Find(waypointAreaName);

        if (waypointArea != null)
        {
            waypoints = new Transform[waypointArea.transform.childCount];

            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = waypointArea.transform.GetChild(i).transform;
            }
        }

        if (waypoints.Length > 0)
        {
            choosenWaypoint = Random.Range(0, waypoints.Length);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, animator.transform.position);

            animator.SetFloat("distance", distance);
        }
        else
        {
            animator.SetFloat("distance", 20.0f);
        }

        if (waypoints.Length > 0)
        {
            if (Vector3.Distance(waypoints[choosenWaypoint].position, animator.transform.position) < 2.0f)
            {
                choosenWaypoint = Random.Range(0, waypoints.Length);
            }
        }

        navMeshAgent.destination = waypoints[choosenWaypoint].position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
