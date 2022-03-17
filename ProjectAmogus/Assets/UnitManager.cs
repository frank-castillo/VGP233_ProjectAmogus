using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitManager : MonoBehaviour
{
    public static List<PlayerChara> units = new List<PlayerChara>();
    public List<RoomPoint> points = new List<RoomPoint>();

    // Update is called once per frame
    void Update()
    {
        //Shortcuts

        //====== ORDERS ====== 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var unit in units)
            {
                unit.Priority = AIPriority.FollowMe;
            }
            AgentUIManager.Instance.SetAllOrdersImagesByKeyboard(AIPriority.FollowMe);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var unit in units)
            {
                unit.Priority = AIPriority.WaitHere;
            }
            AgentUIManager.Instance.SetAllOrdersImagesByKeyboard(AIPriority.WaitHere);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetRoomsPointSelection();
            foreach (var unit in units)
            {
                unit.Priority = AIPriority.SpreadOut;
                if (unit.GetType() != EntityType.Scientist)
                {
                    ChooseTarget(unit.GetNavMeshAgent());
                }
            }
            AgentUIManager.Instance.SetAllOrdersImagesByKeyboard(AIPriority.SpreadOut);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach (var unit in units)
            {
                unit.Priority = AIPriority.CloseIn;
            }
            AgentUIManager.Instance.SetAllOrdersImagesByKeyboard(AIPriority.CloseIn);
        }
        //====================== 
    }

    public void ChooseTarget(NavMeshAgent agent)
    {
        RoomPoint closestTarget = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath Path = null;
        NavMeshPath ShortestPath = null;

        agent.isStopped = false;
        for (int i = 0; i < points.Count; ++i)
        {
            if (points[i] == null || points[i].isVisited == true || points[i].isSelected == true)
            {
                continue;
            }
            Path = new NavMeshPath();

            if (NavMesh.CalculatePath(agent.transform.position, points[i].gameObject.transform.position, agent.areaMask, Path))
            {
                float distance = Vector3.Distance(agent.transform.position, Path.corners[0]);

                for (int j = 1; j < Path.corners.Length; ++j)
                {
                    distance += Vector3.Distance(Path.corners[j - 1], Path.corners[j]);
                }

                if (distance < closestTargetDistance)
                {
                    closestTargetDistance = distance;
                    ShortestPath = Path;
                    closestTarget = points[i];
                }
            }

        }

        if (ShortestPath != null)
        {
            closestTarget.isSelected = true;
            agent.SetPath(ShortestPath);
        }
        else
        {
            agent.GetComponent<PlayerChara>().Priority = AIPriority.FollowMe;
        }
    }

    public void ResetRoomsPointSelection()
    {
        foreach (var roomPoint in points)
        {
            if (roomPoint.isVisited == false)
            {
                roomPoint.isSelected = false;
            }
        }
    }

    public void SetIndividualSpreadOut(NavMeshAgent agent)
    {
        ResetRoomsPointSelection();
        ChooseTarget(agent);
    }
}
