using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Attack : StateMachineBehaviour
{
    [SerializeField] private float speedMultiplier = 2.0f;

    [SerializeField] [Range(0.1f, 1)] private float timeToAttack = 0.75f;

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

        damage = animator.gameObject.GetComponent<EnemyController>().GetDamage();

        backupSpeed = navMeshAgent.speed;

        navMeshAgent.speed *= speedMultiplier;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, animator.transform.position);

            animator.SetFloat("distance", distance);

            navMeshAgent.destination = player.transform.position;

            if(!isAttackOffCooldown && (stateInfo.normalizedTime % 1) > timeToAttack)
            {
                GameController.instance.TakeDamage(damage);
                isAttackOffCooldown = false;
            }
            if (!isAttackOffCooldown && (stateInfo.normalizedTime % 1) < 0.09f)
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
