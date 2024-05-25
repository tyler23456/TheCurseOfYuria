using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;

namespace TCOY.AStar
{
    public class Node : IHeapItem<Node>
    {
        public bool walkable;
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;

        public int hCost;
        public int gCost;
        public Node parent;

        int _heapIndex;

        public int fCost => hCost + gCost;

        public int heapIndex { get { return _heapIndex; } set { _heapIndex = value; } }

        public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
        {
            this.walkable = walkable;
            this.worldPosition = worldPosition;
            this.gridX = gridX;
            this.gridY = gridY;
        }

        public int CompareTo(Node nodeToCompare)
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