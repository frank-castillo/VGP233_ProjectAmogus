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
    protected float health;
    [SerializeField] protected float inCombatTime=12;
    private float timer = 0;
    private bool inCombat = false;

    [SerializeField] protected float healFrequency = 1.0f;
    private float healtTimer;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    protected float maxHealth;

    public float MaxHealth
    {
        get { return health; }
        set { maxHealth = value; }
    }

    protected float moveSpeed;

    public float MoveSpeed
    {
        get { return moveSpeed; } 
        set { moveSpeed = value; }
    }
    protected float turnSpeed;
    
    public float TurnSpeed
    {
        get { return turnSpeed; }
        set { turnSpeed = value; }
    }

    [SerializeField]private AIPriority priority;
    //[SerializeField]protected GameObject originPoint;
    protected NavMeshAgent navMeshAgent;
    public NavMeshAgent GetNavMeshAgent() { return navMeshAgent; }
    private void Update()
    {
        
  
    }
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
    protected StatusMode status;

    public StatusMode Status
    {
        get { return status; }
        set { status = value; }
    }

    public virtual void SetDowned()
    {
        status = StatusMode.Downed;
        Debug.Log(this.gameObject.name + "is Downed!");
    }

    public void SetStunned()
    {
        status = StatusMode.Stunned;
        Debug.Log(this.gameObject.name + "is Stunned!");
    }

    public void SetHealthy()
    {
        status = StatusMode.Healthy;
    }

    public virtual void Damage(float value)
    {
        inCombat = true;
        timer = 0;

        health -= value;
        Debug.Log(this.gameObject.name + "took " + value + " DMG!");
        if (health <= 0.0f)
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

    public void HealBonus()
    {
        if (inCombat)
        {
            timer += Time.deltaTime;
            if (timer >= inCombatTime)
            {
                inCombat = false;
                timer = 0;
            }
        }
        else
        {
            healtTimer += Time.deltaTime;
            if (healtTimer >= healFrequency)
            {
                Heal(1.0f);
                healtTimer = 0;
                //Debug.Log("Unit was healed");
            }
        }
    }
    public void ResetCombatStatus()
    {
        inCombat = true;
        timer = 0;
    }
}
