using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.AStar;

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
            {
                Stay(controller);
            }
                

            if (controller.actionState == State.exit)
            {
                Exit(controller);
                controller.actionState = State.enter;
            }
        }

        public override void FixedUpdateState(IController controller)
        {
            if (controller.actionState == State.stay)
                FixedStay(controller);
        }

        protected virtual void Enter(IController controller) { }
        protected virtual void Stay(IController controller) { }
        protected virtual void FixedStay(IController controller) { }
        protected virtual void Exit(IController controller) { }

        protected void MoveActor(IController controller, float speed = 1f)
        {
            Vector3 waypointPosition = controller.waypoints[controller.waypointIndex].position;
            Vector3 direction = (waypointPosition - controller.rigidbody2D.transform.position).normalized;

            if (direction.x > 0f && controller.rigidbody2D.transform.eulerAngles.y >= 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            else if (direction.x < 0f && controller.rigidbody2D.transform.eulerAngles.y < 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            controller.rigidbody2D.transform.position = Vector3.MoveTowards(controller.rigidbody2D.transform.position, waypointPosition, 7.1f * Time.deltaTime * speed);
        }
        
        public void CheckForEndAutoState(IController controller)
        {
            if (Vector3.Distance(controller.waypoints[controller.waypointIndex].position, controller.position) > IWaypoint.distanceThreshold)
                return;

            if (controller.waypointIndex >= controller.waypoints.Count - 1)
                return;

            controller.waypointIndex++;

            controller.pathfindingConnection = controller.waypoints[controller.waypointIndex].connection;
            controller.SetAction(controller.waypoints[controller.waypointIndex].action);
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