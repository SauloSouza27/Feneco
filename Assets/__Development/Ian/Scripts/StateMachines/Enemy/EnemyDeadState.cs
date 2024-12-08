using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    
    private readonly int DeathHash = Animator.StringToHash("Death");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    private float DeathCooldown = 6f;
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DeathHash, CrossFadeDuration);
        SFXManager.instance.PlaySFX(SFXManager.SFXType.EnemyDeath);
        stateMachine.Weapon.gameObject.SetActive(false);
        //GameObject.Destroy(stateMachine.Target);
    }

    public override void Tick(float deltaTime)
    {
        DeathCooldown -= Time.deltaTime;
        
        if (DeathCooldown <= 0)
        {
            GameObject.Destroy(stateMachine.GameObject());
            return;
        }
        
    }

    public override void Exit()
    {
        
    }
}