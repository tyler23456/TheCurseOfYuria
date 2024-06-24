using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.AStar
{
    public class WaypointManager : MonoBehaviour
    {
        public Waypoint CalculateClosestWaypoint(IPath path)
        {
            Connection connection = (Connection)path.connection;

            if (connection == null)
                return null;

            return (Waypoint)connection.GetClosestWaypoint(path.position);

            /*shortestTransform = transform.GetChild(0);
            shortestDistance = float.PositiveInfinity;
            float distance;

            foreach (Transform t in transform)
            {
                distance = Vector3.Distance(position, t.position);

                if (distance < shortestDistance)
                {
                    shortestTransform = t;
                    shortestDistance = distance;
                }
            }

            return shortestTransform.GetComponent<Waypoint>();*/
        }
    }
}