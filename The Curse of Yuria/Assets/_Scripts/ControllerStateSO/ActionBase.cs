using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class ActionBase : ScriptableObject, IAction
    {
        public string getName => name;

        public void UpdateState(IController controller)
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

        public virtual bool CheckForTransition(IController controller)
        {
            return true;
        }

        public virtual IState GetSisterState()
        {
            return null;
        }

        public virtual void OnDrawGizmosMethod(IController controller)
        {

        }
    }
}