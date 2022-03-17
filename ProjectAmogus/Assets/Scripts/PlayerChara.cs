using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChara : Entity
{
    [SerializeField] EntityType whatType;

    [SerializeField] protected GameObject originPoint;
    [SerializeField] protected GameObject closeInPoint;

    //protected NavMeshAgent navMeshAgent;

    bool enemyFound = false;
    float enemyTimeout = 5.0f;
    bool canAttack = true;
    float atkTimer = 3.0f;
    Transform attackTarget;

    Quaternion pivotAngle = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
    bool lookLeft = false;
    float timeCount = 0.5f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        switch (whatType)
        {
            case EntityType.Scientist:
                {
                    maxHealth = 100.0f;
                    health = maxHealth;
                    moveSpeed = 2.0f;
                    turnSpeed = 2.0f;
                    Priority = AIPriority.None;
                }
                break;
            case EntityType.TrooperA:
                {
                    maxHealth = 100.0f;
                    health = maxHealth;
                    moveSpeed = 2.0f;
                    turnSpeed = 4.0f;
                    Priority = AIPriority.FollowMe;
                }
                break;
            case EntityType.TrooperB:
                {
                    maxHealth = 100.0f;
                    health = maxHealth;
                    moveSpeed = 3.0f;
                    turnSpeed = 3.0f;
                    Priority = AIPriority.FollowMe;
                }
                break;
            case EntityType.TrooperC:
                {
                    maxHealth = 100.0f;
                    health = maxHealth;
                    moveSpeed = 4.0f;
                    turnSpeed = 2.0f;
                    Priority = AIPriority.FollowMe;
                }
                break;
            default:
                {
                    maxHealth = 100.0f;
                    health = maxHealth;
                    moveSpeed = 2.0f;
                    turnSpeed = 2.0f;
                    Priority = AIPriority.None;
                }
                break;
        }
    }

    public void EnemyFound(Transform target)
    {
        enemyFound = true;
        attackTarget = target;
    }
    public void WaitHere()
    {
        navMeshAgent.isStopped = true;
    }
    public void CloseIn()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(closeInPoint.transform.position);
       pivotAngle = Quaternion.Euler((closeInPoint.transform.position - transform.position).normalized);

        //if(navMeshAgent.remainingDistance<=navMeshAgent.stoppingDistance)
        //{
        //    navMeshAgent.angularSpeed = 0;
        //    pivotAngle = Quaternion.LookRotation((UnitManager.units.Find(x => x.whatType == EntityType.Scientist).transform.forward ),navMeshAgent.transform.forward);

        //}
    }
    public void EnemyLost()
    {
        enemyFound = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        UnitManager.units.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status != StatusMode.Downed)
        {
            if (whatType != EntityType.Scientist)
            {
                switch (Priority)
                {
                    case AIPriority.None:
                        break;
                    case AIPriority.FollowMe:
                        Follow();
                        break;
                    case AIPriority.WaitHere:
                        WaitHere();
                        break;
                    case AIPriority.SpreadOut:
                        break;
                    case AIPriority.CloseIn:
                        CloseIn();
                        break;
                    case AIPriority.AttackClosest:
                        break;
                    case AIPriority.AttackPlayer:
                        break;
                    default:
                        break;
                }
            }

            if (!enemyFound && Priority!=AIPriority.CloseIn)
            {
                if(whatType != EntityType.Scientist)
                {
                transform.rotation = Quaternion.Slerp(Quaternion.Euler(0.0f, pivotAngle.y - 22.5f, 0.0f), Quaternion.Euler(0.0f, pivotAngle.y + 22.5f, 0.0f), timeCount);
                

                if (lookLeft)
                {
                    timeCount -= turnSpeed * Time.deltaTime * 0.1f;
                    if (timeCount <= 0.0f) lookLeft = false;
                }
                else
                {
                    timeCount += turnSpeed * Time.deltaTime * 0.1f;
                    if (timeCount >= 1.0f) lookLeft = true;
                }
                }
            }
            else
            {
                if (canAttack && attackTarget != null)
                {
                    Vector3 direction = (attackTarget.transform.position - transform.position).normalized;
                    Ray ray = new Ray(transform.position, direction);
                    RaycastHit hit;
                    Debug.DrawRay(transform.position, direction, Color.blue);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            hit.collider.gameObject.GetComponent<Entity>().Damage(20.0f);
                            if (hit.collider.gameObject.GetComponent<Entity>().Status == StatusMode.Downed)
                            {
                                enemyFound = false;
                                attackTarget = null;
                            }
                            canAttack = false;
                        }
                    }
                }
                else
                {
                    atkTimer -= Time.deltaTime;
                    if (atkTimer <= 0.0f)
                    {
                        canAttack = true;
                        atkTimer = 1.5f;
                    }

                }
            }
        }
    }

    public void Follow()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(originPoint.transform.position);
        pivotAngle = Quaternion.Euler((originPoint.transform.position - transform.position).normalized);
    }

    public void SetPriority(string newPriority)
    {
        if (newPriority == "FollowMe")
        {
            Priority = AIPriority.FollowMe;
        }
        else if (newPriority == "WaitHere")
        {
            Priority = AIPriority.WaitHere;
        }
        else if (newPriority == "SpreadOut")
        {
            Priority = AIPriority.SpreadOut;
        }
        else if (newPriority == "CloseIn")
        {
            Priority = AIPriority.CloseIn;
        }
        else if (newPriority == "AttackClosest")
        {
            Priority = AIPriority.AttackClosest;
        }
        else if (newPriority == "AttackPlayer")
        {
            Priority = AIPriority.AttackPlayer;
        }
    }

    public EntityType GetType()
    {
        return whatType;
    }
    private void OnDestroy()
    {
        UnitManager.units.Remove(this);
    }
}
