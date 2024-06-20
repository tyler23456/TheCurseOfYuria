using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class GoalBase : ScriptableObject, IGoal
    {
        [SerializeField] protected GoalBase[] transitionStates;

        public string getName => name;

        public virtual bool CheckForTransition(IController controller)
        {
            return true;
        }

        public void UpdateState(IController controller)
        {
            if (controller.actionState == IState.State.enter)
            {
                controller.actionState = IState.State.stay;
                Enter(controller);
                
            }


            if (controller.actionState == IState.State.stay)
                Stay(controller);

            foreach (GoalBase transitionState in transitionStates)
                if (transitionState.CheckForTransition(controller) == true)
                {
                    controller.actionState = IState.State.exit;
                    controller.goal = transitionState;
                }

            if (controller.actionState == IState.State.exit)
            {
                Exit(controller);
                controller.actionState = IState.State.enter;
            }
        }

        protected virtual void Enter(IController controller) { }
        protected virtual void Stay(IController controller) { }
        protected virtual void Exit(IController controller) { }

        public virtual void OnDrawGizmosMethod(IController controller)
        {

        }
    }
}