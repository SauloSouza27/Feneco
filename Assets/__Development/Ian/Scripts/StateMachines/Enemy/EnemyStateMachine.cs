using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine_1
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public Vector3 startPosition { get; private set; }
    //[field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float WalkPointRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }
    [field: SerializeField] public float IdleCooldown { get; private set; }
    [field: SerializeField] public LayerMask whatIsGround { get; private set; }
    
    public GameObject Player {get; private set;}
    public Vector3 Walkpoint;

    private void Start()
    {
        startPosition = transform.position + new Vector3(0, 1f, 0);
        
        Player = GameObject.FindGameObjectWithTag("Player");

        Agent.updatePosition = false;
        Agent.updateRotation = false;
        
        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }
    
    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Walkpoint, 0.5f);
    }
}
