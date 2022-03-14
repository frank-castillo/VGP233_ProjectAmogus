using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static List<PlayerChara> units = new List<PlayerChara>();


    // Update is called once per frame
    void Update()
    {
        //Shortcuts

        //====== ORDERS ====== 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach(var unit in units)
            {
                unit.Priority = AIPriority.FollowMe;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var unit in units)
            {
                unit.Priority = AIPriority.WaitHere;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (var unit in units)
            {
                unit.Priority = AIPriority.SpreadOut;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach (var unit in units)
            {
                unit.Priority = AIPriority.CloseIn;
            }

        }
        //====================== 
    }
}
