using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class GoalBase : GoalSO, IGoal
    {
        [SerializeField] protected GoalBase[] transitionStates;

        public new string name => base.name;

        public override bool CheckForTransition(IController controller)
        {
            return true;
        }

        public override void UpdateState(IController controller)
        {
            if (controller.goalState == IState.State.enter)
            {
                controller.goalState = IState.State.stay;
                Enter(controller);
                
            }


            if (controller.goalState == IState.State.stay)
                Stay(controller);

            foreach (GoalBase transitionState in transitionStates)
                if (transitionState.CheckForTransition(controller) == true)
                {
                    controller.SetGoal(transitionState);
                }

            if (controller.goalState == IState.State.exit)
            {
                Exit(controller);
                controller.goalState = IState.State.enter;
            }
        }

        protected virtual void Enter(IController controller) { }
        protected virtual void Stay(IController controller) { }
        protected virtual void Exit(IController controller) { }

        public override void OnDrawGizmosMethod(IController controller)
        {

        }
    }
}