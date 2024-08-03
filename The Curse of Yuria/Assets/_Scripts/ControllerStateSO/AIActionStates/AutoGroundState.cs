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
            if (controller.waypoints.Count == 0 || controller.waypointIndex >= controller.waypoints.Count)
                return;

            if (Vector3.Distance(controller.position, controller.destination) < controller.stopDistance)
            {
                controller.animator.SetInteger("State", 0);
                return;
            }

            MoveActor(controller);
                
            Vector2 path2D = controller.waypoints[controller.waypointIndex].position;
            Vector2 position = controller.position;
            Vector2 direction = (path2D - position).normalized;

            controller.animator.SetInteger("State", 0);

            if (direction.x > 0f)
                controller.animator.SetInteger("State", 2);

            else if (direction.x < 0f)
                controller.animator.SetInteger("State", 2);

            CheckForEndSAutoState(controller);
        }
    }
}