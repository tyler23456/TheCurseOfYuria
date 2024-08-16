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
            controller.SetAction(StateDatabase.Instance.GetAction("ResetState"));

            controller.waypoints.Clear();
            controller.waypointIndex = 0;
            controller.rigidbody2D.isKinematic = false;
            controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        protected override void Stay(IController controller)
        {
            
        }

        protected override void Exit(IController controller)
        {
            controller.rigidbody2D.isKinematic = true;
            controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            controller.rigidbody2D.velocity = Vector2.zero;      
        }
    }
}
