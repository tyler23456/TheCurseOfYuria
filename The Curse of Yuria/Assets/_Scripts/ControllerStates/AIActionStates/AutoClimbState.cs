using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoClimbState", menuName = "AutoActionStates/AutoClimbState")]
    public class AutoClimbState : ActionBase
    {
        ControllerStatesSFX stepSFX = new ControllerStatesSFX();

        protected override void Enter(IController controller)
        {
            controller.animator.SetInteger("State", 5);
            controller.actor.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        
        protected override void Stay(IController controller)
        {
            if (controller.idleState == 1)
                return;

            if (controller.waypoints.Count == 0 || controller.waypointIndex >= controller.waypoints.Count)
                return;

            float distance = Vector3.Distance(controller.position, controller.target.position);

            float speed = 1f;
            if (distance > IController.goDistance * 2f)
                speed = 1.5f;

            if (distance > IController.goDistance)
                controller.isAutoMovementPaused = false;
            else if (distance < IController.stopDistance)
                controller.isAutoMovementPaused = true;

            if (controller.isAutoMovementPaused)
                return;

            MoveActor(controller, speed * 0.5f);
            CheckForEndAutoState(controller);
        }

        protected override void Exit(IController controller)
        {
            controller.animator.SetInteger("State", controller.idleState);
        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("ClimbState");
        }
    }
}