using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.AStar
{
    public class Unit : MonoBehaviour
    {
        Transform target;
        float speed = 0.1f;
        Vector3[] path;
        int index;

        void Start()
        {
            target = Global.instance.allies[0].getGameObject.transform;
            StartCoroutine(CheckForPath());
        }
        
        IEnumerator CheckForPath()
        {
            while (true)
            {
                PathRequester.RequestPath(transform.position, target.position, OnPathFound);
                yield return new WaitForSeconds(0.25f);
            }
        }

        void OnPathFound(Vector3[] wayPoints, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                path = wayPoints;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }

        IEnumerator FollowPath()
        {
            if (path.Length == 0)
                yield break;

            Vector3 currentWayPoint = path[0];

            while (true)
            {
                if (transform.position == currentWayPoint)
                {
                    index++;
                    if (index >= path.Length)
                        yield break;
                    currentWayPoint = path[index];
                }
                transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed);
                yield return new WaitForFixedUpdate();
            }
        }

        public void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = index; i < path.Length; i++)
                {
                    Gizmos.color = Color.red / 2f;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == index)
                        Gizmos.DrawLine(transform.position, path[i]);
                    else
                        Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}