using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "NormalState", menuName = "ControllerStates/NormalState")]
    public class NormalState : NormalStateBase
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
