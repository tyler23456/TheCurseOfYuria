using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "HostileState", menuName = "GoalStates/HostileState")]
    public class HostileState : GoalBase
    {
        protected override void Enter(IController controller)
        {
            controller.idleState = 1;
            controller.animator.SetInteger("State", controller.idleState);
            controller.actor.getATBGuage.RaisePriority();
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);
        }

        protected override void Exit(IController controller)
        {
            controller.idleState = 0;
            controller.animator.SetInteger("State", controller.idleState);
            controller.actor.getATBGuage.LowerPriority();
        }
    }
}

