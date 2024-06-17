using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "BattleState", menuName = "ControllerStates/BattleState")]
    public class BattleState : NormalStateBase
    {
        protected override void Enter(IStateController controller)
        {

        }

        protected override void Stay(IStateController controller)
        {
            base.Stay(controller);
        }

        protected override void Exit(IStateController controller)
        {

        }
    }
}
