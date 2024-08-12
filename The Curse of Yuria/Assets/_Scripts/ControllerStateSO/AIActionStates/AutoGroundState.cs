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
            
            float distance = Vector3.Distance(controller.position, controller.waypoints[controller.waypoints.Count - 1].position);

            float speed = 1f;
            if (distance > controller.goDistance * 2f)
                speed = 1.5f;

            if (distance > controller.goDistance)
                controller.isAutoMovementPaused = false;
            else if (distance < controller.stopDistance)
                controller.isAutoMovementPaused = true;

            if (controller.isAutoMovementPaused)
            {
                controller.animator.SetInteger("State", 0);
                return;
            }

            MoveActor(controller, speed);
                
            Vector2 path2D = controller.waypoints[controller.waypointIndex].position;
            Vector2 position = controller.position;
            Vector2 direction = (path2D - position).normalized;

            if (direction.x > 0f)
                controller.animator.SetInteger("State", 2);

            else if (direction.x < 0f)
                controller.animator.SetInteger("State", 2);

            CheckForEndAutoState(controller);
        }
    }
}