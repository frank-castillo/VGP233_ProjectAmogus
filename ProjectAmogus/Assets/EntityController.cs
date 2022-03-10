using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] bool canControl = false;
    Rigidbody targetRB;

    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetRB != null && canControl)
        {
            if (Input.GetKey(KeyCode.W))
            {
                targetRB.AddForce(new Vector3(0.0f, 0.0f, 0.1f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.A))
            {
                targetRB.AddForce(new Vector3(-0.1f, 0.0f, 0.0f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.S))
            {
                targetRB.AddForce(new Vector3(0.0f, 0.0f, -0.1f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.D))
            {
                targetRB.AddForce(new Vector3(0.1f, 0.0f, 0.0f), ForceMode.Impulse);
            }
        }
    }
}
