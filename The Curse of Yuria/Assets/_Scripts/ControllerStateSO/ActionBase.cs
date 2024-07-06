using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class ActionBase : ScriptableObject
    {
        public new string name => base.name;

        public virtual void UpdateState(IController controller)
        {
            if (controller.actionState == ActionState.State.enter)
            {
                controller.actionState = ActionState.State.stay;
                Enter(controller);
            }

            if (controller.actionState == ActionState.State.stay)
                Stay(controller);

            if (controller.actionState == ActionState.State.exit)
            {
                Exit(controller);
                controller.actionState = ActionState.State.enter;
            }
        }

        protected virtual void Enter(IController controller) { }
        protected virtual void Stay(IController controller) { }
        protected virtual void Exit(IController controller) { }

        public virtual bool CheckForTransition(IController controller)
        {
            return true;
        }

        public virtual ActionState GetSisterState()
        {
            return null;
        }

        public virtual void OnDrawGizmosMethod(IController controller)
        {

        }
    }
}