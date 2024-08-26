using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Pathfinding;

namespace TCOY.ControllerStates
{
    public abstract class GoalBase : GoalState
    {
        [SerializeField] protected GoalState[] transitionStates;

        public new string name => base.name;

        public override bool CheckForTransition(IController controller)
        {
            return true;
        }

        public override void UpdateState(IController controller)
        {
            if (controller.goalState == State.enter)
            {
                controller.goalState = State.stay;
                Enter(controller);
            }

            if (controller.goalState == State.stay)
                Stay(controller);

            if (controller.goalState == State.exit)
            {
                Exit(controller);
                controller.goalState = State.enter;
            }
        }

        public override IEnumerator CheckForPath(IController controller)
        {
            while (true)
            {
                if (controller.waypointIndex < controller.waypoints.Count - 1) //remove later
                    yield return null;

                if (controller.animator.GetInteger("MovePriority") < int.MaxValue) //|| controller.actor.enabled == false)
                    yield return null;

                PathRequester.RequestPath(controller, controller.target);

                yield return new WaitForSeconds(0.1f);
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