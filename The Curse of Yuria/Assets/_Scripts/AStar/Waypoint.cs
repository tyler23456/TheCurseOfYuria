using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;

namespace TCOY.AStar
{
    public class Waypoint : MonoBehaviour, IHeapItem<Waypoint>, IWaypoint
    {
        [SerializeField] WaypointManager waypointManager; 
        [SerializeField] List<Connection> connectedWaypoints;
        [SerializeField] List<Waypoint> waypoints;

        int _heapIndex;

        public int hCost;
        public int gCost;
        public Waypoint parent;

        public int fCost => hCost + gCost;
        public int heapIndex { get { return _heapIndex; } set { _heapIndex = value; } }

        public List<Waypoint> getNeighbors => waypoints;
        public Vector2 position => transform.position;

        void Reset()
        {
            if (transform.parent.GetComponent<WaypointManager>() == null)
                transform.parent.gameObject.AddComponent<WaypointManager>();

            if (waypointManager == null)
                waypointManager = transform.parent.GetComponent<WaypointManager>();
        }

        void OnValidate()
        {
            waypoints.Clear();

            foreach (Connection connection in connectedWaypoints)
                waypoints.Add(connection.waypoint);
        }

        public int CompareTo(Waypoint nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }

        public List<IConnection> GetConnections()
        {
            List<IConnection> results = new List<IConnection>();

            foreach (Connection connection in connectedWaypoints)
                    results.Add(connection);

            return results;
        }
    }
}