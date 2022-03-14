using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawner : MonoBehaviour
{
    [SerializeField] GameObject swarmPrefab;
    [SerializeField] int howManySwarms;

    // Start is called before the first frame update
    void Start()
    {
        var radius = 5.0f;
        for(int i = 0; i < howManySwarms; ++i)
        {
            var swarm  = Instantiate(swarmPrefab, (Random.onUnitSphere * radius) + transform.position, Random.rotation);
            swarm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            swarm.transform.position = new Vector3(swarm.transform.position.x, -10.0f, swarm.transform.position.z);
            swarm.transform.parent = transform.parent;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
