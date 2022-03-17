using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType
{
    None,
    OrangeKey, 
    YellowKey,
    PinkKey
}
public class Key : MonoBehaviour
{
    public KeyType type;
    public Objective objective;
    public ObjectiveEvent whatHappenedWhenPickedUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EntityController>() != null)
        {
            EntityController player = other.GetComponent<EntityController>();

            player.keys.Add(type);
            //Add objective to open the door
            Debug.Log(type.ToString() + " was picked up!.");
            //Debug.Log("Objective Added: "+ objective.text);
            AgentUIManager.Instance.DeleteObjective(whatHappenedWhenPickedUp);
            AgentUIManager.Instance.AddNewObjective(objective);

            Destroy(this.gameObject);
        }
    }
}
