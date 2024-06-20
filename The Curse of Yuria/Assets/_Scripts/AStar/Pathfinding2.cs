using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using TCOY.Heap;
using System;

namespace TCOY.AStar
{
    public class Pathfinding2 : MonoBehaviour
    {
        PathRequester pathRequester;
        WaypointManager waypointManager;

        public void Awake()
        {
            pathRequester = GetComponent<PathRequester>();
            waypointManager = GetComponent<WaypointManager>();
        }

        internal void StartFindPath(Vector3 pathStart, Vector3 pathEnd, IPath path)
        {
            StartCoroutine(FindPath(pathStart, pathEnd, path));
        }

        IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition, IPath path)
        {
            Vector3[] results = new Vector3[0];

            Waypoint startNode = waypointManager.CalculateClosestWaypoint(startPosition);
            Waypoint targetNode = waypointManager.CalculateClosestWaypoint(targetPosition);
            Heap<Waypoint> openSet = new Heap<Waypoint>(waypointManager.transform.childCount);
            List<Waypoint> closedSet = new List<Waypoint>();
            path.pathSuccess = false;

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Waypoint currentNode = openSet.RemoveFirst();


                closedSet.Add(currentNode);

                if (currentNode.GetInstanceID() == targetNode.GetInstanceID())
                {
                    path.pathSuccess = true;
                    break;
                }

                foreach (Waypoint neighbor in currentNode.getNeighbors)
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    int newMovementCost = currentNode.gCost + GetDistance(currentNode, neighbor);

                    if (newMovementCost < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCost;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            if (path.pathSuccess)
                TraversePath(startNode, targetNode, path);

            pathRequester.FinishedProcessingPath();
            yield return new WaitForSecondsRealtime(0.1f);
        }

        void TraversePath(Waypoint startNode, Waypoint endNode, IPath path)
        {
            path.waypoints.Clear();
            Waypoint currentNode = endNode;

            while (currentNode.GetInstanceID() != startNode.GetInstanceID())
            {
                path.waypoints.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.waypoints.Reverse();
        }

        Vector3[] simplifyPath(List<Node> path)
        {
            List<Vector3> wayPoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;

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
    }
}
