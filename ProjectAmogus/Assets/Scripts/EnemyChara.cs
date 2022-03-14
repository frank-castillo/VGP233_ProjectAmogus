using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChara : Entity
{
    [SerializeField] EntityType whatType;
    bool isAsleep = true;
    float aiTimer = 5.0f;
    bool canAttack = true;
    float atkTimer = 1.0f;

    Transform playerRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<EntityController>().gameObject.transform;

        switch(whatType)
        {
            case EntityType.Swarm:
                {
                    MaxHealth = 40.0f;
                    Health = MaxHealth;
                    MoveSpeed = 2.0f;
                    TurnSpeed = 2.0f;
                    Priority = AIPriority.AttackClosest;
                }
                break;
            case EntityType.Monitor:
                {
                    MaxHealth = 120.0f;
                    Health = MaxHealth;
                    MoveSpeed = 4.0f;
                    TurnSpeed = 4.0f;
                    Priority = AIPriority.AttackPlayer;
                }
                break;
            case EntityType.SuperMonitor:
                {
                    MaxHealth = 250.0f;
                    Health = MaxHealth;
                    MoveSpeed = 4.0f;
                    TurnSpeed = 4.0f;
                    Priority = AIPriority.AttackPlayer;
                }
                break;
            default:
                break;
        }
    }

    public void WakeEnemyUp()
    {
        isAsleep = false;
    }

    public void MakeEnemySleep()
    {
        isAsleep = true;
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        WakeEnemyUp();
    }

    public override void SetDowned()
    {
        status = StatusMode.Downed;
        Debug.Log(this.gameObject.name + "is Downed!");
        Object.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAsleep)
        {
            switch (Priority)
            {
                case AIPriority.AttackClosest:
                    {
                        //Prioritizing Target

                        var targets = FindObjectsOfType<PlayerChara>();
                        List<PlayerChara> validTargets = new List<PlayerChara>();

                        foreach(PlayerChara pChar in targets)
                        {
                            if (pChar.Status != StatusMode.Downed) validTargets.Add(pChar);
                        }

                        Transform nearest = targets[0].transform;

                        foreach (PlayerChara pchar in validTargets)
                        {
                            var temp = Vector3.Distance(this.transform.position, pchar.transform.position);
                            if (temp < Vector3.Distance(this.transform.position, nearest.position)) nearest = pchar.transform;
                        }

                        var dist = Vector3.Distance(this.transform.position, nearest.position);

                        if (dist > 2.0f && dist < 10.0F)
                        {
                            //Approach
                            this.transform.position = Vector3.MoveTowards(this.transform.position, nearest.position, MoveSpeed * Time.deltaTime);

                            if(canAttack)
                            {
                                if (dist <= 3.0f)
                                {
                                    //Melee Attack
                                    Vector3 direction = (nearest.position - transform.position).normalized;
                                    Ray ray = new Ray(transform.position, direction);
                                    RaycastHit hit;
                                    Debug.DrawRay(transform.position, direction, Color.red);

                                    if (Physics.Raycast(ray, out hit))
                                    {
                                        if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Trooper"))
                                        {
                                            hit.collider.gameObject.GetComponent<Entity>().Damage(20.0f);
                                            canAttack = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                atkTimer -= Time.deltaTime;
                                if (atkTimer <= 0.0f)
                                {
                                    canAttack = true;
                                    atkTimer = 1.0f;
                                }
                            }
                        }
                        else
                        {
                            aiTimer -= Time.deltaTime;
                            if (aiTimer <= 0.0f)
                            {
                                isAsleep = true;
                                aiTimer = 5.0f;
                            }
                        }
                    }
                    break;
                case AIPriority.AttackPlayer:
                    {
                        var dist = Vector3.Distance(this.transform.position, playerRef.position);

                        if (dist > 1.0f && dist < 10.0F)
                        {
                            //Approach
                            this.transform.position = Vector3.MoveTowards(this.transform.position, playerRef.position, MoveSpeed * Time.deltaTime);
                        }
                        else if (dist <= 1.0f)
                        {
                            //Attack Range
                        }
                        else
                        {
                            //Disengage
                            aiTimer -= Time.deltaTime;
                            if (aiTimer < 0.0f)
                            {
                                isAsleep = true;
                                aiTimer = 10.0f;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
