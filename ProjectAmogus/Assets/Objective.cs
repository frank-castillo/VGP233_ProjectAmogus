using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveEvent
{
    YellowDoorOpened, 
    OrangeDoorOpenned,
    PinkDoorOpenned,
    YelloKeyPickedUp,
    OrangeKeyPickepUp,
    PinkKeyPickedUp
}
[System.Serializable]
public class Objective 
{
    public ObjectiveEvent objectiveEvent;
    public string text = string.Empty;
    

}
