using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;
using UnityEditor;
using System.Collections.ObjectModel;

namespace TCOY.AStar
{
    [ExecuteAlways]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Connection : MonoBehaviour, IConnection, IHeapItem<Connection>
    {
        [SerializeField] [HideInInspector] new EdgeCollider2D collider;

        [SerializeField] Waypoint firstWaypoint;
        [SerializeField] Waypoint secondWaypoint;
        [SerializeField] ActionState action;
        [SerializeField] List<Connection> connections;

        int _heapIndex;

        public Vector2 position => transform.position;

        public int hCost;
        public int gCost;
        public Connection parent;

        public int fCost => hCost + gCost;
        public int heapIndex { get { return _heapIndex; } set { _heapIndex = value; } }

        public IWaypoint getFirstWaypoint => firstWaypoint;
        public IWaypoint getSecondWaypoint => secondWaypoint;
        public ActionState getAction => action;
        public ReadOnlyCollection<Connection> getNeighbors => connections.AsReadOnly();

        public void Awake()
        {
            connections = new List<Connection>();
            connections.Clear();
            connections.AddRange(firstWaypoint.getConnections);
            connections.AddRange(secondWaypoint.getConnections);
            connections.RemoveAll(i => i.Equals(this));
        }

        public void ConnectWaypoints(Waypoint firstWaypoint, Waypoint secondWaypoint)
        {
            this.firstWaypoint = firstWaypoint;
            this.secondWaypoint = secondWaypoint;
            RefreshTransform();
            RefreshCollider();
        }

        public Waypoint GetSharedWaypoint(Connection connection)
        {
            if (firstWaypoint.getConnections.Contains(connection))
                return firstWaypoint;
            else if (secondWaypoint.getConnections.Contains(connection))
                return secondWaypoint;
            return null;
        }

        public void AddLineRenderer()
        {
           
        }

        public void RefreshTransform()
        {
            Vector2 averagePosition = (firstWaypoint.position + secondWaypoint.position) / 2f;
            transform.position = averagePosition;
        }

        public void Update()
        {
            if (!transform.hasChanged)
                return;

            transform.hasChanged = false;

            RefreshTransformOfAttachedWaypoints();
            RefreshCollider();
        }

        public void RefreshTransformOfAttachedWaypoints()
        {
            firstWaypoint.UpdateTransform();
            secondWaypoint.UpdateTransform();
        }

        public void RefreshCollider()
        {
            if (collider == null)
                collider = gameObject.GetComponent<EdgeCollider2D>();

            collider.isTrigger = true;
            collider.edgeRadius = 0.1f;

            Vector2 halfLength = (secondWaypoint.position - firstWaypoint.position) / 2f;

            collider.points = new Vector2[] { -halfLength, halfLength };
        }


        public IWaypoint GetOtherWaypoint(IWaypoint thisWaypoint)
        {
            return thisWaypoint.Equals(firstWaypoint) ? secondWaypoint : firstWaypoint;
        }

        public IWaypoint GetClosestWaypoint(Vector2 position)
        {
            return Vector2.Distance(position, firstWaypoint.position) > Vector2.Distance(position, secondWaypoint.position)? secondWaypoint : firstWaypoint;
        }

        void OnDestroy()
        {
            firstWaypoint.Remove(secondWaypoint);
        }

        public int CompareTo(Connection nodeToCompare)
        {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.clear;
            Gizmos.DrawLine(firstWaypoint.transform.position, secondWaypoint.transform.position);
            var p1 = firstWaypoint.transform.position;
            var p2 = secondWaypoint.transform.position;
            var thickness = 3;
            Handles.DrawBezier(p1, p2, p1, p2, Color.yellow, null, thickness);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.clear;
            Gizmos.DrawLine(firstWaypoint.transform.position, secondWaypoint.transform.position);
            var p1 = firstWaypoint.transform.position;
            var p2 = secondWaypoint.transform.position;
            var thickness = 3;
            Handles.DrawBezier(p1, p2, p1, p2, Color.green, null, thickness);
        }
    }
}