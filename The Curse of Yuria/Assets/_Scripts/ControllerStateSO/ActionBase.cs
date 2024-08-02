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

        protected void MoveActor(IController controller, float speed)
        {
            Vector3 waypointPosition = controller.waypoints[controller.waypointIndex].position;
            Vector3 direction = (waypointPosition - controller.rigidbody2D.transform.position).normalized;

            if (direction.x > 0f && controller.rigidbody2D.transform.eulerAngles.y >= 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            else if (direction.x < 0f && controller.rigidbody2D.transform.eulerAngles.y < 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            controller.rigidbody2D.transform.position = Vector3.MoveTowards(controller.rigidbody2D.transform.position, waypointPosition, IWaypoint.distanceThreshold / 2f);
        }

        public void CheckForEndState(IController controller)
        {
            IWaypoint waypoint = controller.waypoints[controller.waypointIndex];

            if (Vector3.Distance(waypoint.position, controller.position) > IWaypoint.distanceThreshold)
                return;

            if (controller.waypointIndex >= controller.waypoints.Count - 1)
                return;

            controller.previousWaypoint = waypoint;
            controller.waypointIndex++;

            IConnection connection = waypoint.FindConnection(controller.waypoints[controller.waypointIndex]);

            if (connection == null || connection.getAction == null)
                controller.SetAction(StateDatabase.Instance.GetAction("AutoGroundState"));
            else
                controller.SetAction(connection.getAction);
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