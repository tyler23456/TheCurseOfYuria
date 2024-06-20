using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "JumpState", menuName = "PlayerActionStates/JumpState")]
    public class JumpState : ActionBase
    {
        protected override void Enter(IController controller)
        {
            base.Enter(controller);
            controller.velocity += Vector2.up * 100;
            controller.action = StateDatabase.Instance.GetAction("GroundState");
            controller.actionState = IState.State.exit;
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);
        }

        protected override void Exit(IController controller)
        {
            base.Exit(controller);

            
        }
    }
}