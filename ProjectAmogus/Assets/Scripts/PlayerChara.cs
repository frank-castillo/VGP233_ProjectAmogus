using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChara : Entity
{
    [SerializeField] EntityType whatType;
    Transform nextWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        switch (whatType)
        {
            case EntityType.Scientist:
            {
                MaxHealth = 100.0f;
                Health = MaxHealth;
                MoveSpeed = 2.0f;
                TurnSpeed = 2.0f;
                Priority = AIPriority.None;
                
            }
            break;
            case EntityType.TrooperA:
            {
                MaxHealth = 100.0f;
                Health = MaxHealth;
                MoveSpeed = 2.0f;
                TurnSpeed = 4.0f;
                Priority = AIPriority.FollowMe;
            }
            break;
            case EntityType.TrooperB:
            {
                MaxHealth = 100.0f;
                Health = MaxHealth;
                MoveSpeed = 3.0f;
                TurnSpeed = 3.0f;
                Priority = AIPriority.FollowMe;
            }
            break;
            case EntityType.TrooperC:
            {
                MaxHealth = 100.0f;
                Health = MaxHealth;
                MoveSpeed = 4.0f;
                TurnSpeed = 2.0f;
                Priority = AIPriority.FollowMe;
            }
            break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (Priority)
        {
            case AIPriority.None:
                break;
            case AIPriority.FollowMe:
                Follow();
                break;
            case AIPriority.WaitHere:
                break;
            case AIPriority.SpreadOut:
                break;
            case AIPriority.CloseIn:
                break;
            case AIPriority.AttackClosest:
                break;
            case AIPriority.AttackPlayer:
                break;
            default:
                break;
        }
    }

    public void Follow()
    {
        navMeshAgent.SetDestination(originPoint.transform.position);
    }
}
