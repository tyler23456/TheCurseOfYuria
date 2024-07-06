using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class GoalBase : ScriptableObject
    {
        [SerializeField] protected GoalState[] transitionStates;

        public new string name => base.name;

        public virtual bool CheckForTransition(IController controller)
        {
            return true;
        }

        public virtual void UpdateState(IController controller)
        {
            if (controller.goalState == GoalState.State.enter)
            {
                controller.goalState = GoalState.State.stay;
                Enter(controller);
                
            }


            if (controller.goalState == GoalState.State.stay)
                Stay(controller);

            foreach (GoalState transitionState in transitionStates)
                if (transitionState.CheckForTransition(controller) == true)
                {
                    controller.SetGoal(transitionState);
                }

            if (controller.goalState == GoalState.State.exit)
            {
                Exit(controller);
                controller.goalState = GoalState.State.enter;
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