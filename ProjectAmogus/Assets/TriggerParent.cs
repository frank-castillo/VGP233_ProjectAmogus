using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParent : MonoBehaviour
{
    Entity entityRef;
    [SerializeField] EntityType whatType;

    // Start is called before the first frame update
    void Start()
    {
        switch (whatType)
        {
            case EntityType.Scientist:
                entityRef = GetComponentInParent<PlayerChara>();
                break;
            case EntityType.TrooperA:
                entityRef = GetComponentInParent<PlayerChara>();
                break;
            case EntityType.TrooperB:
                entityRef = GetComponentInParent<PlayerChara>();
                break;
            case EntityType.TrooperC:
                entityRef = GetComponentInParent<PlayerChara>();
                break;
            case EntityType.Swarm:
                entityRef = GetComponentInParent<EnemyChara>();
                break;
            case EntityType.Monitor:
                entityRef = GetComponentInParent<EnemyChara>();
                break;
            case EntityType.SuperMonitor:
                entityRef = GetComponentInParent<EnemyChara>();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerChara>() != null && entityRef == null)
        {
            if(whatType == EntityType.Swarm || whatType == EntityType.Monitor || whatType ==  EntityType.SuperMonitor)
            {
                var myEnemy = entityRef as EnemyChara;
                if(myEnemy != null) myEnemy.WakeEnemyUp();
            }
        }

        if (other.gameObject.GetComponent<EnemyChara>() != null && entityRef != null)
        {
            if (whatType == EntityType.Scientist || whatType == EntityType.TrooperA || whatType == EntityType.TrooperB ||
                whatType == EntityType.TrooperC)
            {
                var myAlly = entityRef as PlayerChara;
                if (myAlly != null) myAlly.EnemyFound(other.transform);
            }
        }
    }
}
