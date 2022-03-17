using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMove : MonoBehaviour
{
    public Vector3 originPoint;
    public Vector3 destination;
    public float moveSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if(originPoint != null && destination != null)
        {
            var moveDir = originPoint - destination;
            this.transform.Translate(new Vector3(moveDir.x, 0.0f, moveDir.y) * moveSpeed * Time.deltaTime);
        }
    }
}
