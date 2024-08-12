using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoJumpState", menuName = "AutoActionStates/AutoJumpState")]
    public class AutoJumpState : ActionBase
    {
        const int subWaypointCount = 2;

        protected override void Enter(IController controller)
        {
            if (controller.waypoints.Count == 0 || controller.waypointIndex >= controller.waypoints.Count)
                EndTheState(controller);

            base.Enter(controller);

            Vector2 waypoint = controller.waypoints[controller.waypointIndex].position;

            controller.subWaypointIndex = 0;
            controller.subWaypoints[0] = new Vector2((controller.position.x + waypoint.x) / 2f, Mathf.Max(controller.position.y, waypoint.y) + 1.5f);
            controller.subWaypoints[1] = waypoint;
            controller.isPathfindingPaused = true;
        }

        protected override void Stay(IController controller)
        {
            if (controller.waypoints.Count == 0 || controller.waypointIndex >= controller.waypoints.Count)
                EndTheState(controller);

            base.Stay(controller);

            if (Vector3.Distance(controller.position, controller.subWaypoints[controller.subWaypointIndex]) <= IWaypoint.distanceThreshold)
                controller.subWaypointIndex = Mathf.Clamp(controller.subWaypointIndex + 1, 0, subWaypointCount - 1);

            Vector3 waypointPosition = controller.subWaypoints[controller.subWaypointIndex];
            Vector3 direction = (waypointPosition - controller.rigidbody2D.transform.position).normalized;

            if (direction.x > 0f && controller.rigidbody2D.transform.eulerAngles.y >= 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            else if (direction.x < 0f && controller.rigidbody2D.transform.eulerAngles.y < 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            if (controller.subWaypointIndex == 1)
            {
                controller.rigidbody2D.transform.position = Vector3.MoveTowards(controller.rigidbody2D.transform.position, waypointPosition, 10f * Time.deltaTime);
                CheckForEndAutoState(controller);
            }        
            else
                controller.rigidbody2D.transform.position = Vector3.MoveTowards(controller.rigidbody2D.transform.position, waypointPosition, 19f * Time.deltaTime);
        }

        void EndTheState(IController controller)
        {
            controller.SetAction(StateDatabase.Instance.GetAction("AutoGroundState"));
        }

        protected override void Exit(IController controller)
        {
            base.Exit(controller);
            controller.isPathfindingPaused = false;
        }
    }
}