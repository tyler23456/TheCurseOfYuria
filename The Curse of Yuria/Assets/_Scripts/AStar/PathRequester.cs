using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.AStar
{
    public class PathRequester : MonoBehaviour
    {
        static PathRequester instance;

        Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
        PathRequest currentPathRequest;
        Pathfinding2 pathfinding;

        bool isProcessingPath;

        private void Awake()
        {
            instance = this;
            pathfinding = GetComponent<Pathfinding2>();
        }

        public static void RequestPath (Vector3 pathStart, Vector3 pathEnd, IPath path)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, path);
            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();
        }

        void TryProcessNext()
        {
            if (!isProcessingPath && pathRequestQueue.Count > 0)
            {
                currentPathRequest = pathRequestQueue.Dequeue();
                isProcessingPath = true;
                pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd, currentPathRequest.path);
            }
        }

        public void FinishedProcessingPath()
        {
            isProcessingPath = false;
            TryProcessNext();
        }

        public struct PathRequest
        {
            public Vector3 pathStart;
            public Vector3 pathEnd;
            public IPath path;

            public PathRequest(Vector3 start, Vector3 end, IPath path)
            {
                this.pathStart = start;
                this.pathEnd = end;
                this.path = path;
            }
        }

    }
}