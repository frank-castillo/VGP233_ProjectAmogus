using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This Class is going to be an abstract class which has the variables that all Entities have

public enum AIPriority
{
    None = -1,
    FollowMe,
    WaitHere,
    SpreadOut,
    CloseIn,
    AttackClosest,
    AttackPlayer
}

public enum EntityType
{
    Scientist,
    TrooperA,
    TrooperB,
    TrooperC,
    Swarm,
    Monitor,
    SuperMonitor
}

public enum StatusMode
{
    Healthy,
    Stunned,
    Downed
}

public class Entity : MonoBehaviour
{
    private float health;
    
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private float maxHealth;

    public float MaxHealth
    {
        get { return health; }
        set { maxHealth = value; }
    }

    private float moveSpeed;

    public float MoveSpeed
    {
        get { return moveSpeed; } 
        set { moveSpeed = value; }
    }
    private float turnSpeed;
    
    public float TurnSpeed
    {
        get { return turnSpeed; }
        set { turnSpeed = value; }
    }

    [SerializeField]private AIPriority priority;
    [SerializeField]protected GameObject originPoint;
    protected NavMeshAgent navMeshAgent;
    public NavMeshAgent GetNavMeshAgent() { return navMeshAgent; }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public AIPriority Priority
    {
        get { return priority; }
        set { priority = value; }
    }
    //private EntityType type; 
    
    //public EntityType Type
    //{
    //    get { return type; }
    //}
    private StatusMode status;

    public StatusMode Status
    {
        get { return status; }
        set { status = value; }
    }

    public void SetDowned()
    {
        status = StatusMode.Downed;
    }

    public void SetStunned()
    {
        status = StatusMode.Stunned;
    }

    public void SetHealth()
    {
        status = StatusMode.Healthy;
    }

    public void Damage(float value)
    {
        health -= value;
        if(health <= 0.0f)
        {
            health = 0.0f;
            SetDowned();
        }
    }

    public void Heal(float value)
    {
        health += value;        
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }
}