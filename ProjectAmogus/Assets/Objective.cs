using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveEvent
{
    YellowDoorOpened, 
    OrangeDoorOpenned,
    PinkDoorOpenned,
    YellowKeyPickedUp,
    OrangeKeyPickedUp,
    PinkKeyPickedUp
}
[System.Serializable]
public class Objective 
{
    public ObjectiveEvent destroyCondition;
    public string text = string.Empty;
    

}
