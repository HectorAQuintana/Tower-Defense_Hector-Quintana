using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsSystem : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypoints = new List<Transform>();

    public int GetWaypointsCount => waypoints.Count;

    public Vector3 GetWaypointPosition(int index)
    {
        if(waypoints.Count - 1 < index)
        {
            Debug.LogError("Index outside waypoints size, returning last waypoint position");
            return waypoints[waypoints.Count - 1].position;
        }

        return waypoints[index].position;
    }
}
