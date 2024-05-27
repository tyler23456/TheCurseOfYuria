using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;

namespace TCOY.AStar
{
    public class Waypoint : MonoBehaviour, IHeapItem<Waypoint>
    {
        [SerializeField] WaypointManager waypointManager; 
        [SerializeField] List<Waypoint> connectedWaypoints;

        int _heapIndex;

        public int hCost;
        public int gCost;
        public Waypoint parent;

        public int fCost => hCost + gCost;
        public int heapIndex { get { return _heapIndex; } set { _heapIndex = value; } }

        public List<Waypoint> getNeighbors => connectedWaypoints;

        void Reset()
        {
            if (transform.parent.GetComponent<WaypointManager>() == null)
                transform.parent.gameObject.AddComponent<WaypointManager>();

            if (waypointManager == null)
                waypointManager = transform.parent.GetComponent<WaypointManager>();
        }

        void Start()
        {

        }

        void Update()
        {

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
    }
}