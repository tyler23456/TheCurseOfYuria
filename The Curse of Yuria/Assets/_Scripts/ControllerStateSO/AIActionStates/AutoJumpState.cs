using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoJumpState", menuName = "AutoActionStates/AutoJumpState")]
    public class AutoJumpState : ActionBase
    {
        Vector2 displacement = Vector2.zero;
        Vector2 initialVelocity = Vector2.zero;
        Vector2 finalVelocity = Vector2.zero;
        Vector2 acceleration = Vector2.zero;
        float h = 5f;
        float t = 2f;

        protected override void Enter(IController controller)
        {
            base.Enter(controller);

            /*displacement = controller.waypoints[controller.index] - controller.position;
            displacement += displacement.normalized * 2f;
            acceleration = Physics2D.gravity; //potentially air resistance;0
            t = 1f;

            initialVelocity.y = Mathf.Sqrt(-2 * acceleration.y * h);
            initialVelocity.x = displacement.x / (Mathf.Sqrt(-2 * h / acceleration.y) + Mathf.Sqrt(2 * (displacement.y - h) / acceleration.y));

            controller.velocity = Vector2.zero;
            controller.rigidbody2D.AddForce(initialVelocity * 2f, ForceMode2D.Impulse);*/
            
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);

            if (controller.waypoints.Count == 0 || controller.index >= controller.waypoints.Count)
                return;

            if (Vector3.Distance(controller.position, controller.destination) < controller.stopDistance)
                return;

            Vector2 path2D = controller.waypoints[controller.index];
            Vector2 position = controller.position;
            Vector2 direction = (path2D - position).normalized;

            Vector3 currentWayPoint = controller.waypoints[controller.index];

            controller.animator.SetInteger("State", 0);

            if (direction.x > 0.1f)
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                controller.animator.SetInteger("State", 3);
                //controller.velocity += Vector2.right * controller.speed;
                controller.rigidbody2D.AddForce(Vector2.right * controller.speed);
            }
            else if (direction.x < 0.1f)
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                controller.animator.SetInteger("State", 3);
                //controller.velocity += Vector2.left * controller.speed;
                controller.rigidbody2D.AddForce(Vector2.left * controller.speed);
            }


        }

        protected override void Exit(IController controller)
        {
            base.Exit(controller);
        }
    }
}