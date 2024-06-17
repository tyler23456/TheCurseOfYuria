using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class StateBase : ScriptableObject, IState
    {
        [SerializeField] protected StateBase[] transitionStates;

        bool isEntering = true;


        public virtual bool CheckForTransition(IStateController controller)
        {
            return true;
        }

        public void UpdateState(IStateController controller)
        {
            if (isEntering)
            {
                Enter(controller);
                isEntering = false;
            } 

            Stay(controller);

            foreach (StateBase transitionState in transitionStates)
                if (transitionState.CheckForTransition(controller) == true)
                {
                    Exit(controller);
                    isEntering = true;
                    controller.state = transitionState;
                }
        }

        protected virtual void Enter(IStateController controller) { }
        protected virtual void Stay(IStateController controller) { }
        protected virtual void Exit(IStateController controller) { }

        public virtual void OnDrawGizmosMethod(IStateController controller)
        {

        }
    }
}