using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class ActionBase : ActionState
    {
        public new string name => base.name;

        public override void UpdateState(IController controller)
        {
            if (controller.actionState == State.enter)
            {
                controller.actionState = State.stay;
                Enter(controller);
            }

            if (controller.actionState == State.stay)
                Stay(controller);

            if (controller.actionState == State.exit)
            {
                Exit(controller);
                controller.actionState = State.enter;
            }
        }

        protected virtual void Enter(IController controller) { }
        protected virtual void Stay(IController controller) { }
        protected virtual void Exit(IController controller) { }

        protected bool MoveActor(IController controller, float speed)
        {
            if (controller.waypoints.Count == 0 || controller.waypointIndex >= controller.waypoints.Count)
                return false;

            Vector3 waypointPosition = controller.waypoints[controller.waypointIndex];
            Vector3 direction = (waypointPosition - controller.rigidbody2D.transform.position).normalized;

            if (direction.x > 0f && controller.rigidbody2D.transform.eulerAngles.y >= 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            else if (direction.x < 0f && controller.rigidbody2D.transform.eulerAngles.y < 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            controller.rigidbody2D.transform.position = Vector3.MoveTowards(controller.rigidbody2D.transform.position, waypointPosition, IWaypoint.distanceThreshold / 2f);
            
            return true;
        }

        public override bool CheckForTransition(IController controller)
        {
            return true;
        }

        public override ActionState GetSisterState()
        {
            return null;
        }

        public override void OnDrawGizmosMethod(IController controller)
        {

        }
    }
}