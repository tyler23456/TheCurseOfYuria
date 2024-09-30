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
            if (controller.goal.name == "PatrolState")
                return;

            controller.animator.SetInteger("State", 0);

            if (controller.goal.name == "BattleState")
                return;

            if (!IPlayerControls.hasPlayerMoved)
                return;

            if (controller.waypoints.Count == 0 || controller.waypointIndex >= controller.waypoints.Count)
                return;
            
            float distance = Vector3.Distance(controller.position, controller.target.position);

            float speed = 0.5f;
            if (distance > IController.goDistance * 2f)
                speed = 0.75f;

            if (distance > IController.goDistance)
                controller.isAutoMovementPaused = false;
            else if (distance < IController.stopDistance)
                controller.isAutoMovementPaused = true;

            if (controller.isAutoMovementPaused)
                return;

            MoveActor(controller, IPlayerControls.isPlayerRunning ? speed * 2f : speed);
                
            Vector2 path2D = controller.waypoints[controller.waypointIndex].position;
            Vector2 position = controller.position;
            Vector2 direction = (path2D - position).normalized;

            if (direction.x > 0.01f)
                controller.animator.SetInteger("State", IPlayerControls.isPlayerRunning ? 2 : 1);

            else if (direction.x < -0.01f)
                controller.animator.SetInteger("State", IPlayerControls.isPlayerRunning ? 2 : 1);

            CheckForEndAutoState(controller);
        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("GroundState");
        }
    }
}