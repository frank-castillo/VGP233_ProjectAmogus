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

        UnitManager.units.Add(this);
    }

    // Update is called once per frame
    void Update()
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
      
    }

    public void Follow()
    {
        navMeshAgent.SetDestination(originPoint.transform.position);
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
