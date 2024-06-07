using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_NPC_Idle : StateMachineBehaviour
{
    private GameObject player = null;

    private NavMeshAgent navMeshAgent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = animator.transform.GetComponent<NavMeshAgent>();
    }

    //  OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC npcScript = animator.transform.GetComponent<NPC>();

        if (player != null && !npcScript.isDelivered)
        {
            float distance = Vector3.Distance(player.transform.position, animator.transform.position);

            if (distance > 3 && npcScript.acceptedMission)
            {
                animator.SetBool("isEscort", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC npcScript = animator.transform.GetComponent<NPC>();

        if (npcScript.isQuestUpdated)
        {
            navMeshAgent.destination = npcScript.CallbackObject.transform.GetChild(2).position;
        }
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
