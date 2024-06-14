using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.AStar
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class ArialUnit : MonoBehaviour
    {
        Transform target;
        float speed = 0.1f;
        float moveSpeed = 1f;
        Vector3[] path;
        int index;
        Animator animator;
        Rigidbody2D rigidBody2D;

        void Start()
        {
            target = AllieManager.Instance.First().obj.transform;
            StartCoroutine(CheckForPath());
            animator = GetComponent<Animator>();
            rigidBody2D = GetComponent<Rigidbody2D>();
        }
        
        IEnumerator CheckForPath()
        {
            while (true)
            {
                if (animator?.GetInteger("MovePriority") < int.MaxValue || GetComponent<IActor>()?.enabled == false)
                {
                    yield return new WaitForSeconds(0.1f);
                    continue;
                }

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
                if (GetComponent<IActor>()?.enabled == false)
                {
                    yield return new WaitForSeconds(0.1f);
                    continue;
                }

                Vector2 path2D = new Vector2(path[0].x, path[0].y);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                Vector2 direction = (path2D - position).normalized;

                if (transform.position == currentWayPoint)
                {
                    index++;
                    if (index >= path.Length)
                        yield break;
                    currentWayPoint = path[index];
                }

                if (JumpTest())
                    Jump(direction);

                Move(direction);

                yield return new WaitForFixedUpdate();
            }
        }

        Vector2 velocity = Vector3.zero;
        void Move(Vector2 direction)
        {
            animator.SetInteger("State", 0);

            if (direction.x > 0f)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                animator.SetInteger("State", 2);
                velocity += Vector2.right * moveSpeed;;
            }
            else if (direction.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                animator.SetInteger("State", 2);
                velocity += Vector2.left * moveSpeed;
            }
        }

        void Jump(Vector2 direction)
        {

            velocity += Vector2.up * 100;

        }

        bool JumpTest()
        {
            bool forwardObstruction = Physics2D.Raycast(transform.position + transform.up * 0.1f, transform.right, 0.8f, LayerMask.GetMask("TileCollision"));
            bool grounded = Physics2D.Raycast(transform.position, -transform.up, 0.2f, LayerMask.GetMask("TileCollision"));

            return grounded && forwardObstruction;
        }

        void FixedUpdate()
        {
            rigidBody2D.AddForce(velocity, ForceMode2D.Impulse);
            velocity = Vector2.zero;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position + transform.up * 0.1f, transform.position + transform.up * 0.1f + transform.right * 0.8f);
            Gizmos.DrawLine(transform.position, transform.position - transform.up * 0.2f);

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