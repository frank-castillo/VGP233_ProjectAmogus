using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public KeyType keyRequired;
    public Objective objective;
    public List<ObjectiveEvent> objectivesToDelteOnceItOppened = new List<ObjectiveEvent>();
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
                foreach(var x in objectivesToDelteOnceItOppened)
                {
                AgentUIManager.Instance.DeleteObjective(x);
                }

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
