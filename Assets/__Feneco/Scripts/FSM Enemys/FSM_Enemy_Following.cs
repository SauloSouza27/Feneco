using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Enemy_Following : StateMachineBehaviour
{
    [SerializeField] private float speedOverride = 4.0f;

    private GameObject player = null;

    private NavMeshAgent navMeshAgent;

    private float backupSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");

        navMeshAgent = animator.transform.GetComponent<NavMeshAgent>();

        backupSpeed = navMeshAgent.speed;

        navMeshAgent.speed = speedOverride;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, animator.transform.position);

            animator.SetFloat("distance", distance);

            if (navMeshAgent.enabled)
            {
                navMeshAgent.destination = player.transform.position;
            }
        }
        else
        {
            animator.SetFloat("distance", 20.0f);
        }        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.speed = backupSpeed;
    }

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
