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

        internal void StartFindPath(IPath user, IPath target)
        {
            StartCoroutine(FindPath(user, target));
        }

        IEnumerator FindPath(IPath user, IPath target)
        {
            if (!((IController)user).animator.GetBool("IsGrounded"))
                yield return null;

            Waypoint startNode = waypointManager.CalculateClosestWaypoint(user);
            Waypoint targetNode = waypointManager.CalculateClosestWaypoint(target);

            Heap<Waypoint> openSet = new Heap<Waypoint>(waypointManager.transform.childCount);
            List<Waypoint> closedSet = new List<Waypoint>();
            user.pathSuccess = false;

            if (startNode == null || targetNode == null)
            {
                pathRequester.FinishedProcessingPath();
                yield break;
            }

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Waypoint currentNode = openSet.RemoveFirst();


                closedSet.Add(currentNode);

                if (currentNode.position == targetNode.position)
                {
                    user.pathSuccess = true;
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

            if (user.pathSuccess)
                TraversePath(user, target, startNode, targetNode);

            pathRequester.FinishedProcessingPath();
            yield break;
        }

        void TraversePath(IPath user, IPath target, Waypoint startNode, Waypoint endNode)
        {
            user.waypoints.Clear();
            user.index = 0;
            Waypoint currentNode = endNode;

            Vector2 previousDirection = (target.position - currentNode.position).normalized;
            Vector2 currentDirection = Vector2.zero;

            while (currentNode.position != startNode.position)
            {
                currentDirection = (currentNode.position - currentNode.parent.position).normalized;

                //helps to prevent directions that overlap one another
                if (Vector2.Dot(previousDirection, currentDirection) > -0.8f)
                    user.waypoints.Add(currentNode.position);

                currentNode = currentNode.parent;
                previousDirection = currentDirection;
            }

            currentDirection = (currentNode.position - user.position).normalized;
            if (Vector2.Dot(previousDirection, currentDirection) > -0.8f)
                user.waypoints.Add(currentNode.position);

            user.waypoints.Reverse();
            user.waypoints.Add(target.position);
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
    }
}
