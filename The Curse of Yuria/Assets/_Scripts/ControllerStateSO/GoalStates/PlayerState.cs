using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "PlayerState", menuName = "GoalStates/PlayerState")]
    public class PlayerState : GoalBase
    {
        protected override void Enter(IController controller)
        {
            controller.pathfindingConnection = null;
            controller.waypoints.Clear();
            controller.waypointIndex = 0;

            controller.rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            controller.SetAction(controller.action.GetSisterState());
        }

        protected override void Stay(IController controller)
        {
            
        }

        protected override void Exit(IController controller)
        {
            controller.rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            controller.rigidbody2D.velocity = Vector2.zero;

            controller.SetAction(controller.action.GetSisterState());
        }
    }
}
