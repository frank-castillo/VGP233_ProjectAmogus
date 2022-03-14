using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParent : MonoBehaviour
{
    Component entityRef;

    // Start is called before the first frame update
    void Start()
    {
        entityRef = GetComponentInParent<Entity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerChara>() != null && entityRef != null)
        {
            var myEnemy = entityRef as EnemyChara;
            if (myEnemy != null) myEnemy.WakeEnemyUp();
        }

        if (other.gameObject.GetComponent<EnemyChara>() != null && entityRef != null)
        {
            var myAlly = entityRef as PlayerChara;
            if (myAlly != null) myAlly.EnemyFound(other.transform);
        }
    }
}
