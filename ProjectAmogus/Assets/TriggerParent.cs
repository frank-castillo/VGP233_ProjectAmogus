using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParent : MonoBehaviour
{
    EnemyChara enemyRef;

    // Start is called before the first frame update
    void Start()
    {
        enemyRef = GetComponentInParent<EnemyChara>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerChara>() != null && enemyRef != null)
        {
            enemyRef.WakeEnemyUp();
        }
    }
}
