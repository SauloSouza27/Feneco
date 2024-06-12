using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Enemy_Attack : StateMachineBehaviour
{
    [SerializeField] private float speedOverride = 0.0f;

    [SerializeField] [Range(0.151f, 1)] private float timeToAttack = 0.75f;

    private GameObject player = null;

    private bool isAttackOffCooldown = true;

    private NavMeshAgent navMeshAgent;

    private float backupSpeed;

    private int damage;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");

        navMeshAgent = animator.transform.GetComponent<NavMeshAgent>();

        damage = animator.gameObject.GetComponent<EnemyController>().damage;

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

            if(distance < 1.5f)
            {
                navMeshAgent.destination = animator.transform.position;
            }
            else
            {
                navMeshAgent.destination = player.transform.position;
            }

            if(isAttackOffCooldown && (stateInfo.normalizedTime % 1) > timeToAttack && distance < 1.6)
            {
                GameController.instance.TakeDamage(damage);
                isAttackOffCooldown = false;
            }
            if (!isAttackOffCooldown && (stateInfo.normalizedTime % 1) < 0.15f)
            {
                isAttackOffCooldown = true;
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
