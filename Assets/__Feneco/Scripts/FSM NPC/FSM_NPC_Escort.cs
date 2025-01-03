using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_NPC_Escort : StateMachineBehaviour
{
    private GameObject player = null;

    private NavMeshAgent navMeshAgent;

    private NPC npcScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = animator.transform.GetComponent<NavMeshAgent>();
        npcScript = animator.transform.GetComponent<NPC>();
    }

    //  OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, animator.transform.position);

            if (distance <= 3 && !npcScript.isDelivered)
            {
                animator.SetBool("isEscort", false);
            }

            if (!npcScript.isDelivered && animator.GetBool("isEscort") && distance > 3)
            {
                navMeshAgent.destination = player.transform.position;
            }

            if (npcScript.isDelivered)
            {
                navMeshAgent.destination = npcScript.CallbackObject.transform.GetChild(2).position;

                navMeshAgent.transform.LookAt(player.transform);

                if(Vector3.Distance(animator.transform.position, npcScript.CallbackObject.transform.GetChild(2).position) <= 1f)
                {
                    animator.SetBool("isEscort", false);
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (!npcScript.isDelivered)
        {
            navMeshAgent.destination = animator.transform.position;
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
