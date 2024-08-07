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
            if (controller.waypoints.Count == 0 || controller.waypointIndex >= controller.waypoints.Count)
                return;

            if (Vector3.Distance(controller.position, controller.destination) < controller.stopDistance)
                return;

            MoveActor(controller, 0.5f);
            CheckForEndAutoState(controller);
        }

        protected override void Exit(IController controller)
        {
            controller.animator.SetInteger("State", 0);
        }
    }
}