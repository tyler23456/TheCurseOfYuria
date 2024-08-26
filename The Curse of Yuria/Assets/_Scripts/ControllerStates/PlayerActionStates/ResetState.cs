using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "ResetState", menuName = "PlayerActionStates/ResetState")]
    public class ResetState : ActionBase
    {
        protected override void Enter(IController controller)
        {
            if (controller.isGrounded)
            {
                controller.SetAction(controller.connection.getAction.GetSisterState());
            }
            else
            {
                controller.SetAction(StateDatabase.Instance.GetAction("FallState"));
            }

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
            return StateDatabase.Instance.GetAction("AutoResetState");
        }
    }
}
