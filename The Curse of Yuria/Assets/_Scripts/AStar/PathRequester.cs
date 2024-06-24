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

        public static void RequestPath (IPath user, IPath target)
        {
            PathRequest newRequest = new PathRequest(user, target);
            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();
        }

        void TryProcessNext()
        {
            if (!isProcessingPath && pathRequestQueue.Count > 0)
            {
                currentPathRequest = pathRequestQueue.Dequeue();
                isProcessingPath = true;
                pathfinding.StartFindPath(currentPathRequest.user, currentPathRequest.target);
            }
        }

        public void FinishedProcessingPath()
        {
            isProcessingPath = false;
            TryProcessNext();
        }

        public struct PathRequest
        {
            public IPath user;
            public IPath target;

            public PathRequest(IPath user, IPath target)
            {
                this.user = user;
                this.target = target;
            }
        }

    }
}