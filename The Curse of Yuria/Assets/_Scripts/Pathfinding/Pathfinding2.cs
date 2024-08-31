using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

namespace TCOY.Pathfinding
{
    public class Pathfinding2 : MonoBehaviour
    {
        [SerializeField] Transform waypointParent;

        PathRequester pathRequester;

        public void Awake()
        {
            pathRequester = GetComponent<PathRequester>();
        }

        internal void StartFindPath(IPath user, IPath target)
        {
            StartCoroutine(FindPath(user, target));
        }

        IEnumerator FindPath(IPath user, IPath target)
        {
            user.pathSuccess = false;

            if (user.isPathfindingPaused)
            {
                pathRequester.FinishedProcessingPath();
                yield break;
            }

            if (!target.isGrounded)
            {
                pathRequester.FinishedProcessingPath();
                yield break;
            }

            Connection startNode = (Connection)user.pathfindingConnection;
            Connection targetNode = (Connection)target.connection;

            Heap<Connection> openSet = new Heap<Connection>(waypointParent.childCount);
            List<Connection> closedSet = new List<Connection>();
           
            if (startNode == null || targetNode == null)
            {
                pathRequester.FinishedProcessingPath();
                yield break;
            }

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Connection currentNode = openSet.RemoveFirst();


                closedSet.Add(currentNode);

                if (currentNode.position == targetNode.position)
                {
                    user.pathSuccess = true;
                    break;
                }

                foreach (Connection neighbor in currentNode.getNeighbors)
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    int newMovementCost = currentNode.gCost + GetDistance(currentNode) + GetDistance(neighbor);

                    if (newMovementCost < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCost;
                        neighbor.hCost = GetDistance(neighbor) + GetDistance(targetNode);
                        neighbor.parent = currentNode;

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            if (user.pathSuccess)
                TraversePath(user, target, startNode, targetNode);

            pathRequester.FinishedProcessingPath();
            yield break;
        }

        
        void TraversePath(IPath user, IPath target, Connection startNode, Connection endNode)
        {
            user.waypoints.Clear();
            user.waypointIndex = 0;

            Connection currentNode = endNode;
            Waypoint currentWaypoint = null;

            while (currentNode.position != startNode.position)
            {
                currentWaypoint = currentNode.GetSharedWaypoint(currentNode.parent);
                user.waypoints.Add(new SimpleWaypoint(currentWaypoint.position, currentNode.parent.getAction, currentNode.parent));
                currentNode = currentNode.parent;
            }

            //user.waypoints.Add(new SimpleWaypoint(user.position, startNode.getAction, startNode));

            user.waypoints.Reverse();
            user.waypoints.Add(new SimpleWaypoint(target.contactPoint, endNode.getAction, endNode));
        }

        Vector3[] simplifyPath(List<Node> path)
        {
            List<Vector3> wayPoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;
            path.Clear();

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
                if (directionNew != directionOld)
                {
                    wayPoints.Add(path[i].worldPosition);
                }
                directionOld = directionNew;
            }
            return wayPoints.ToArray();
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (distanceX > distanceY)
                return 14 * distanceY + 10 * (distanceX - distanceY);
            else
                return 14 * distanceX + 10 * (distanceY - distanceX);
        }

        int GetDistance(Waypoint nodeA, Waypoint nodeB)
        {
            return (int)Vector3.Distance(nodeA.transform.position, nodeB.transform.position);
        }

        int GetDistance(Connection connection)
        {
            return (int)Vector3.Distance(connection.getFirstWaypoint.position, connection.getSecondWaypoint.position);
        }
    }
}
