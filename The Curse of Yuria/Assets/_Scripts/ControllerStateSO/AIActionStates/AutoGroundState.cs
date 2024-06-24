using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoGroundState", menuName = "AutoActionStates/AutoGroundState")]
    public class AutoGroundState : ActionBase
    {
        protected override void Stay(IController controller)
        {
            if (controller.waypoints.Count == 0 || controller.index >= controller.waypoints.Count)
                return;

            if (Vector3.Distance(controller.position, controller.destination) < controller.stopDistance)
                return;

            Vector2 path2D = controller.waypoints[controller.index];
            Vector2 position = controller.position;
            Vector2 direction = (path2D - position).normalized;

            Vector3 currentWayPoint = controller.waypoints[controller.index];

            controller.animator.SetInteger("State", 0);

            if (direction.x > 0f)
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                controller.animator.SetInteger("State", 3);
                //controller.velocity += Vector2.right * controller.speed;
                controller.rigidbody2D.AddForce(Vector2.right * controller.speed);
            }
            else if (direction.x < 0f)
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                controller.animator.SetInteger("State", 3);
                //controller.velocity += Vector2.left * controller.speed;
                controller.rigidbody2D.AddForce(Vector2.left * controller.speed);
            }
        }
    }
}