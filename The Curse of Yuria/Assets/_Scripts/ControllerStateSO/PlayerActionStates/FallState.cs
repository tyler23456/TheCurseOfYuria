using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "FallState", menuName = "PlayerActionStates/FallState")]
    public class FallState : GroundState
    {
        protected override void Enter(IController controller)
        {
            //has Player Fall
        }

        protected override void Stay(IController controller)
        {
            PlayerMovement(controller);

            controller.animator.SetInteger("State", 3);

            if (controller.isGrounded)
                controller.SetAction(StateDatabase.Instance.GetAction("GroundState"));

        }

        protected override void Exit(IController controller)
        {

        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("AutoFallState"); ;
        }
    }
}
