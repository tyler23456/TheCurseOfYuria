using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "GoalStates/PatrolState")]
    public class PatrolState : GoalBase
    {
        Vector2[] waypointPositions = new Vector2[2] { Vector2.left * 3, Vector2.right * 3 };
        float idleDuration = 3f;

        protected override void Enter(IController controller) 
        {
            controller.waypoints.Clear();
            controller.waypointIndex = Random.Range(0, waypointPositions.Length);
            controller.accumulator = Random.Range(0, idleDuration);
            //controller.idleState = 0;
        }


        protected override void Stay(IController controller) 
        {
            Vector3 waypointPosition = controller.origin + waypointPositions[controller.waypointIndex];
            Vector3 direction = (waypointPosition - controller.rigidbody2D.transform.position).normalized;

            if (direction.x > 0f && controller.rigidbody2D.transform.eulerAngles.y < 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            else if (direction.x < 0f && controller.rigidbody2D.transform.eulerAngles.y >= 90f)
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            controller.animator.SetInteger("State", 2);
            controller.rigidbody2D.transform.position = Vector3.MoveTowards(controller.rigidbody2D.transform.position, waypointPosition, 4.1f * Time.deltaTime * 1);

            if (Vector3.Distance(waypointPosition, controller.position) > IWaypoint.distanceThreshold)
                return;

            controller.accumulator += Time.deltaTime;
            controller.animator.SetInteger("State", controller.idleState);

            if (controller.accumulator < idleDuration)
                return;

            controller.accumulator = 0f;

            controller.waypointIndex++;
            controller.waypointIndex = controller.waypointIndex % waypointPositions.Length;
        }

        protected override void Exit(IController controller) 
        {
            
        }
    }
}