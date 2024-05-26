using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using TCOY.Heap;
using System;

namespace TCOY.AStar
{
    public class Pathfinding : MonoBehaviour
    {
        PathRequester pathRequester;
        Grid grid;

        public void Awake()
        {
            pathRequester = GetComponent<PathRequester>();
            grid = GetComponent<Grid>();
        }

        internal void StartFindPath(Vector3 pathStart, Vector3 pathEnd)
        {
            StartCoroutine(FindPath(pathStart, pathEnd));
        }

        IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            Node startNode = grid.NodeFromWorldPoint(startPosition);
            Node targetNode = grid.NodeFromWorldPoint(targetPosition);
            Heap<Node> openSet = new Heap<Node>(grid.maxSize);
            List<Node> closedSet = new List<Node>();
            Vector3[] wayPoints = new Vector3[0];
            bool pathSuccess = false;

            if (startNode.walkable && targetNode.walkable)
            {

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();

                    //----
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    foreach (Node neighbor in grid.GetNeighbors(currentNode))
                    {
                        if (!neighbor.walkable || closedSet.Contains(neighbor))
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
            }
            if (pathSuccess)
                wayPoints = TraversePath(startNode, targetNode);

            pathRequester.FinishedProcessingPath(wayPoints, pathSuccess);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        Vector3[] TraversePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            Vector3[] wayPoints = simplifyPath(path);
            Array.Reverse(wayPoints);
            return wayPoints;
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
    }
}