using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChara : Entity
{
    [SerializeField] EntityType whatType;
    bool isAsleep = true;
    float aiTimer = 3.0f;

    Transform playerRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<EntityController>().gameObject.transform;

        switch(whatType)
        {
            case EntityType.Swarm:
                {
                    MaxHealth = 20.0f;
                    Health = MaxHealth;
                    MoveSpeed = 2.0f;
                    TurnSpeed = 2.0f;
                    Priority = AIPriority.AttackClosest;
                }
                break;
            case EntityType.Monitor:
                {
                    MaxHealth = 75.0f;
                    Health = MaxHealth;
                    MoveSpeed = 4.0f;
                    TurnSpeed = 4.0f;
                    Priority = AIPriority.AttackPlayer;
                }
                break;
            case EntityType.SuperMonitor:
                {
                    MaxHealth = 200.0f;
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

    // Update is called once per frame
    void Update()
    {
        if(!isAsleep)
        {
            switch (Priority)
            {
                case AIPriority.AttackClosest:
                    {
                        //Prioritizing Target

                        var targets = FindObjectsOfType<PlayerChara>();
                        Transform nearest = targets[0].transform;

                        foreach (PlayerChara pchar in targets)
                        {
                            var temp = Vector3.Distance(this.transform.position, pchar.transform.position);
                            if (temp < Vector3.Distance(this.transform.position, nearest.position)) nearest = pchar.transform;
                        }

                        var dist = Vector3.Distance(this.transform.position, nearest.position);

                        if (dist > 0.2f && dist < 10.0F)
                        {
                            //Approach
                            this.transform.position = Vector3.MoveTowards(this.transform.position, nearest.position, MoveSpeed * Time.deltaTime);
                        }
                        else if (dist <= 0.2f)
                        {
                            //Melee Range
                        }
                        else
                        {
                            aiTimer -= Time.deltaTime;
                            if (aiTimer < 0.0f)
                            {
                                isAsleep = true;
                                aiTimer = 10.0f;
                            }
                        }
                    }
                    break;
                case AIPriority.AttackPlayer:
                    {
                        var dist = Vector3.Distance(this.transform.position, playerRef.position);

                        if (dist > 0.2f && dist < 10.0F)
                        {
                            //Approach
                            this.transform.position = Vector3.MoveTowards(this.transform.position, playerRef.position, MoveSpeed * Time.deltaTime);
                        }
                        else if (dist <= 0.2f)
                        {
                            //Melee Range
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