using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoJumpState", menuName = "AutoActionStates/AutoJumpState")]
    public class AutoJumpState : ActionBase
    {
        protected override void Enter(IController controller)
        {
            if (controller.waypoints.Count == 0 || controller.index >= controller.waypoints.Count)
                EndTheState(controller);

            base.Enter(controller);  
        }

        protected override void Stay(IController controller)
        {
            if (controller.waypoints.Count == 0 || controller.index >= controller.waypoints.Count)
                EndTheState(controller);

            base.Stay(controller);

            if (!MoveActor(controller, 4f))
                return;
        }

        void EndTheState(IController controller)
        {
            controller.SetAction(StateDatabase.Instance.GetAction("AutoGroundState"));
        }

        protected override void Exit(IController controller)
        {
            base.Exit(controller);
            controller.rigidbody2D.gravityScale = 1f;
        }
    }
}