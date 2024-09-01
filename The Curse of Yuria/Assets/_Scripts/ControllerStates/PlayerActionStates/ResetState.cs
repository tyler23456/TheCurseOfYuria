using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "ResetState", menuName = "PlayerActionStates/ResetState")]
    public class ResetState : GroundState
    {
        protected override void Enter(IController controller)
        {
            if (controller.isGrounded)
                controller.SetAction(controller.connection.getAction.GetSisterState());
        }

        protected override void Stay(IController controller)
        {
            PlayerMovement(controller);

            controller.animator.SetInteger("State", 3);

            if (controller.isGrounded)
            {
                controller.SetAction(controller.connection.getAction.GetSisterState());
            }
            else if (controller.animator.GetBool("IsGrounded"))
            {
                controller.SetAction(StateDatabase.Instance.GetAction("GroundState"));
            }
                
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
