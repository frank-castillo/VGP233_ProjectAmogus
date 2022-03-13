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
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                targetRB.AddForce(new Vector3(0.0f, 0.0f, 0.01f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                targetRB.AddForce(new Vector3(-0.01f, 0.0f, 0.0f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                targetRB.AddForce(new Vector3(0.0f, 0.0f, -0.01f), ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                targetRB.AddForce(new Vector3(0.01f, 0.0f, 0.0f), ForceMode.Impulse);
            }
        }
    }
}
