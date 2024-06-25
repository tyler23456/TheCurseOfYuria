using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoJumpState", menuName = "AutoActionStates/AutoJumpState")]
    public class AutoJumpState : ActionBase
    {
        float offset = 1.28f / 8f;
        
        Vector2 waypoint;
        
        protected override void Enter(IController controller)
        {
            base.Enter(controller);
            waypoint = controller.waypoints[controller.index];
            controller.rigidbody2D.AddForce(Vector2.up * 4f * controller.speed, ForceMode2D.Impulse);
            controller.velocity = (waypoint - controller.position).normalized;
            controller.animator.SetBool("IsGrounded", false);
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);

            if (controller.waypoints.Count == 0 || controller.index >= controller.waypoints.Count)
                return;

            if (Vector3.Distance(controller.position, controller.destination) < controller.stopDistance)
                return;

            controller.animator.SetInteger("State", 0);

            if (controller.velocity.x > 0f)
            {
                if (controller.position.x < waypoint.x + offset)
                {
                    controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    controller.animator.SetInteger("State", 3);
                    controller.rigidbody2D.AddForce(Vector2.right * controller.speed * 2f);
                }
                else if (controller.animator.GetBool("IsGrounded"))
                {
                    EndTheState(controller);
                }
            }
            else if (controller.velocity.x < 0f)
            {
                if (controller.position.x > waypoint.x - offset)
                {
                    controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                    controller.animator.SetInteger("State", 3);
                    controller.rigidbody2D.AddForce(Vector2.left * controller.speed * 2);
                }
                else if (controller.animator.GetBool("IsGrounded"))
                {
                    EndTheState(controller);
                }
            }
        }

        void EndTheState(IController controller)
        {
            controller.action = StateDatabase.Instance.GetAction("AutoGroundState");
            controller.actionState = IState.State.exit;
        }

        protected override void Exit(IController controller)
        {
            base.Exit(controller);
        }
    }
}