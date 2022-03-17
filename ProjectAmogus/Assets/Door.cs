using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public KeyType keyRequired;
    public Objective objective;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EntityController>() !=null)
        {
            EntityController player = other.GetComponent<EntityController>();

            if (player.HasKey(keyRequired))
            {
                //open door
                player.keys.Remove(keyRequired);
                this.gameObject.SetActive(false);
                AgentUIManager.Instance.DeleteObjective(objective.objectiveEvent);
            }
            else
            {
                AgentUIManager.Instance.AddNewObjective(objective);
                // Add objective
                Debug.Log(objective.text);
            }

        }
    }
}
