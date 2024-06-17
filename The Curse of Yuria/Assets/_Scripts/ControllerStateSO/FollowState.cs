using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.AStar;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "FollowState", menuName = "ControllerStates/FollowState")]
    public class FollowState : StateBase
    {
        Transform target;
        Vector3[] path = new Vector3[0];
        int index;

        public override bool CheckForTransition(IStateController controller)
        {
            return Vector3.Distance(controller.rigidbody2D.transform.position, target.position) < controller.battleDistance;
        }

        protected override void Enter(IStateController controller)
        {
            base.Enter(controller);
            
            target = AllieManager.Instance[0].obj.transform;
            path = new Vector3[0];
            controller.actor.StartCoroutine(CheckForPath(controller));
        }

        protected override void Stay(IStateController controller)
        {
            if (path.Length == 0 || index >= path.Length || Vector3.Distance(controller.rigidbody2D.transform.position, target.position) < controller.stopDistance)
                return;

            Vector3 currentWayPoint = path[index];

            Vector2 path2D = new Vector2(path[index].x, path[index].y);
            Vector2 position = controller.rigidbody2D.transform.position;
            Vector2 direction = (path2D - position).normalized;

            controller.animator.SetInteger("State", 0);

            if (direction.x > 0f)
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                controller.animator.SetInteger("State", 2);
                controller.velocity += Vector2.right * controller.speed;
            }
            else if (direction.x < 0f)
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                controller.animator.SetInteger("State", 3);
                controller.velocity += Vector2.left * controller.speed;
            }

            if (controller.rigidbody2D.transform.position == currentWayPoint)
                index++;
        }

        protected override void Exit(IStateController controller)
        {
            base.Exit(controller);

            controller.actor.StopCoroutine(CheckForPath(controller));
        }


        IEnumerator CheckForPath(IStateController controller)
        {
            while (true)
            {
                if (controller.animator.GetInteger("MovePriority") < int.MaxValue || controller.actor.enabled == false)
                    yield return null;

                PathRequester.RequestPath(controller.rigidbody2D.transform.position, target.position, OnPathFound);

                yield return new WaitForSeconds(0.5f);
            }
        }

        void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
        {
            if (waypoints == null || !pathSuccessful)
                return;

            List<Vector3> waypointList = new List<Vector3>();
            waypointList.AddRange(waypoints);
            waypointList.Add(target.transform.position);

            path = waypointList.ToArray();
        }

        public override void OnDrawGizmosMethod(IStateController controller)
        {
            if (path != null)
            {
                for (int i = index; i < path.Length; i++)
                {
                    Gizmos.color = Color.red / 2f;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == index)
                        Gizmos.DrawLine(controller.rigidbody2D.transform.position, path[i]);
                    else
                        Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}