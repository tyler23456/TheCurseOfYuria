using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoPathState", menuName = "AutoActionStates/AutoPathState")]
    public class AutoPathState : ActionBase
    {
        protected override void Enter(IController controller)
        {
            //automatically checks path and has AI fall to a connection if no connection exists.
            if (controller.isGrounded && controller.connection != null)
            {
                controller.SetAction(controller.connection.getAction);
                controller.pathfindingConnection = controller.connection;
            }    
            else
                controller.SetAction(StateDatabase.Instance.GetAction("AutoFallState"));
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);
        }

        protected override void Exit(IController controller)
        {

        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("FallState");
        }
    }
}
