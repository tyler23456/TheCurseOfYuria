using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class ActionBase : ActionSO, IAction
    {
        public new string name => base.name;

        public override void UpdateState(IController controller)
        {
            if (controller.actionState == IState.State.enter)
            {
                controller.actionState = IState.State.stay;
                Enter(controller);
            }

            if (controller.actionState == IState.State.stay)
                Stay(controller);

            if (controller.actionState == IState.State.exit)
            {
                Exit(controller);
                controller.actionState = IState.State.enter;
            }
        }

        protected virtual void Enter(IController controller) { }
        protected virtual void Stay(IController controller) { }
        protected virtual void Exit(IController controller) { }

        public override bool CheckForTransition(IController controller)
        {
            return true;
        }

        public override IState GetSisterState()
        {
            return null;
        }

        public override void OnDrawGizmosMethod(IController controller)
        {

        }
    }
}