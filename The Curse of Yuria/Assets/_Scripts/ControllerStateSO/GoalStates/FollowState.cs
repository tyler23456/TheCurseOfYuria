using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.AStar;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "FollowState", menuName = "GoalStates/FollowState")]
    public class FollowState : GoalBase
    {
        public override bool CheckForTransition(IController controller)
        {
            return Vector3.Distance(controller.origin, controller.destination) < controller.battleDistance;
        }

        protected override void Enter(IController controller)
        {
            base.Enter(controller);
            
            controller.destination = AllieManager.Instance[0].obj.transform.position;
            controller.waypoints.Clear();
            controller.actor.StartCoroutine(CheckForPath(controller));
        }

        protected override void Stay(IController controller)
        {
            if (controller.waypoints.Count == 0 || controller.index >= controller.waypoints.Count || Vector3.Distance(controller.origin, controller.destination) < controller.stopDistance)
                return;

            Vector3 currentWayPoint = controller.waypoints[controller.index].position;

            Vector2 path2D = controller.waypoints[controller.index].position;
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
                controller.index++;
        }

        protected override void Exit(IController controller)
        {
            base.Exit(controller);

            controller.actor.StopCoroutine(CheckForPath(controller));
        }


        IEnumerator CheckForPath(IController controller)
        {
            while (true)
            {
                if (controller.animator.GetInteger("MovePriority") < int.MaxValue || controller.actor.enabled == false)
                    yield return null;

                PathRequester.RequestPath(controller.origin, controller.destination, (IPath)controller);

                yield return new WaitForSeconds(0.5f);
            }
        }

        public override void OnDrawGizmosMethod(IController controller)
        {
            if (controller.waypoints != null)
            {
                for (int i = controller.index; i < controller.waypoints.Count; i++)
                {
                    Gizmos.color = Color.red / 2f;
                    Gizmos.DrawCube(controller.waypoints[i].position, Vector3.one);

                    if (i == controller.index)
                        Gizmos.DrawLine(controller.rigidbody2D.transform.position, controller.waypoints[i].position);
                    else
                        Gizmos.DrawLine(controller.waypoints[i - 1].position, controller.waypoints[i].position);
                }
            }
        }
    }
}