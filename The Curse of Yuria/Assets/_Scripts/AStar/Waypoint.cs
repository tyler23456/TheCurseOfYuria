using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;
using UnityEditor;

namespace TCOY.AStar
{
    [ExecuteAlways]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Waypoint : MonoBehaviour, IHeapItem<Waypoint>, IWaypoint
    {
        [SerializeField] WaypointManager waypointManager;
        [SerializeField] List<Waypoint> neighbors;
        [SerializeField] List<Connection> connections;
        
        int _heapIndex;

        public int hCost;
        public int gCost;
        public Waypoint parent;

        public Color color { get; set; } = IWaypoint.defaultColor;

        public int fCost => hCost + gCost;
        public int heapIndex { get { return _heapIndex; } set { _heapIndex = value; } }

        public List<IWaypoint> getNeighbors => neighbors.ConvertAll(i => (IWaypoint)i);
        public List<IConnection> getConnections => connections.ConvertAll(i => (IConnection)i);
        public Vector2 position => transform.position;

        void Reset()
        {
            if (transform.parent.GetComponent<WaypointManager>() == null)
                transform.parent.gameObject.AddComponent<WaypointManager>();

            if (waypointManager == null)
                waypointManager = transform.parent.GetComponent<WaypointManager>();
        }

        public void Add(Waypoint otherWaypoint)
        {
            Connection connection = new GameObject(this.name + " Connected to " + otherWaypoint.name).AddComponent<Connection>();
            connection.transform.parent = transform.parent;
            connection.ConnectWaypoints(this, otherWaypoint);
            this.neighbors.Add(otherWaypoint);
            this.connections.Add(connection);
            otherWaypoint.neighbors.Add(this);
            otherWaypoint.connections.Add(connection);
        }

        public void Remove(Waypoint otherWaypoint)
        {
            _Remove(otherWaypoint);
        }

        public void RemoveAndDestroyConnection(Waypoint otherWaypoint)
        {
            Connection connection = _Remove(otherWaypoint);
            DestroyImmediate(connection.gameObject);
        }

        Connection _Remove(Waypoint otherWaypoint)
        {
            Connection connection = this.connections.Find(i => i.GetOtherWaypoint(this).Equals(otherWaypoint));
            this.neighbors.Remove(otherWaypoint);
            this.connections.Remove(connection);
            otherWaypoint.neighbors.Remove(this);
            otherWaypoint.connections.Remove(connection);
            return connection;
        }

        public void RemoveAllAndDestroyConnection()
        {
            for (int i = neighbors.Count - 1; i >= 0; i--)
                this.RemoveAndDestroyConnection(neighbors[i]);
        }

        void Update()
        {
            if (!transform.hasChanged)
                return;

            transform.hasChanged = false;
            UpdateTransform();
        }

        public void UpdateTransform()
        {
            foreach (Connection connection in connections)
            {
                connection.RefreshTransform();
                connection.RefreshCollider();
            }
                
        }

        void OnDestroy()
        {
            RemoveAllAndDestroyConnection();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            IController controller = collision.GetComponent<IController>();

            if (controller == null || controller.waypoints.Count == 0 || controller.goal.getName == "PlayerState") //may need to change player state later
                return;

            Vector2 position = transform.position;

            if (controller.index >= controller.waypoints.Count || position != controller.waypoints[controller.index])
                return;

            controller.index++;

            Connection targetConnection = null;
            foreach (Connection connection in connections)
                if (controller.waypoints.Contains(connection.GetOtherWaypoint(this).position))
                {
                    targetConnection = connection;
                    break;
                }
            
            if (targetConnection == null || targetConnection.getAction == null)
                return;

            controller.actionState = IState.State.exit;
            controller.action.UpdateState(controller);
            controller.action = targetConnection.getAction;
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

        void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, 1f);
        }
    }
}