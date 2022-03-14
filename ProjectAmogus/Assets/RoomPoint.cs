using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPoint : MonoBehaviour
{
   public bool isVisited = false;
    public bool isSelected = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerChara>()!=null)
        {
            isVisited = true;
            GetComponent<Renderer>().material.color = Color.yellow;
        }    

        
    }
}
