using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.AStar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundUnit : MonoBehaviour
    {
        static float proximity = 5f;

        Transform target;
        float moveSpeed = 2f;
        Vector3[] path = new Vector3[0];
        int index;
        Vector2 velocity = Vector2.zero;
        Animator animator;
        Rigidbody2D rigidBody2D;

        void Start()
        {
            target = AllieManager.Instance[0].obj.transform;
            StartCoroutine(CheckForPath());
            animator = GetComponent<Animator>();
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        IEnumerator CheckForPath()
        {
            while (true)
            {
                if (animator?.GetInteger("MovePriority") < int.MaxValue)
                    continue;

                if (Vector3.Distance(transform.position, target.position) < proximity)
                    yield return new WaitForSeconds(0.1f);

                PathRequester.RequestPath(transform.position, target.position, OnPathFound);
                yield return new WaitForSeconds(0.1f);
            }
        }

        void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
        {
            if (waypoints == null)
                return;

            List<Vector3> waypointList = new List<Vector3>();
            waypointList.AddRange(waypoints);
            waypointList.Add(target.transform.position);
            if (pathSuccessful)
            {
                this.path = waypointList.ToArray();
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
                Vector2 path2D = new Vector2(path[index].x, path[index].y);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                Vector2 direction = (path2D - position).normalized;

                if (Vector3.Distance(transform.position, target.position) < proximity)
                    yield break;

                if (transform.position == currentWayPoint)
                {
                    index++;
                    if (index >= path.Length)
                        yield break;
                    currentWayPoint = path[index];
                }

                Move(direction);

                yield return new WaitForFixedUpdate();
            }
        }
        
        void Move(Vector2 direction)
        {
            animator?.SetInteger("State", 0);

            if (direction.x > 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                animator?.SetInteger("State", 2);
                velocity += Vector2.right * moveSpeed;
            }
            else if (direction.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                animator?.SetInteger("State", 3);
                velocity += Vector2.left * moveSpeed;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Waypoint waypoint = collision.GetComponent<Waypoint>();

            if (waypoint == null)
                return;

            //need really good jump checker
            velocity += Vector2.up * 100f;
        }

        void FixedUpdate()
        {
            rigidBody2D.AddForce(velocity, ForceMode2D.Impulse);
            velocity = Vector2.zero;
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