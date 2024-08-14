using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "BattleState", menuName = "GoalStates/BattleState")]
    public class BattleState : GoalBase
    {
        protected override void Enter(IController controller)
        {
            controller.idleState = 1;
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);
        }

        protected override void Exit(IController controller)
        {
            controller.idleState = 0;
        }
    }
}
