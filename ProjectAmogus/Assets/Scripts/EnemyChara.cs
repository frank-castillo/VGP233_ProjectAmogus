using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChara : Entity
{
    [SerializeField] EntityType whatType;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
