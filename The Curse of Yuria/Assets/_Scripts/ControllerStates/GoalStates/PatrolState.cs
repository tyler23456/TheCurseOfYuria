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
            controller.actor.getATBGuage.EnterStasis();

            controller.waypoints.Clear();
            controller.waypointIndex = Random.Range(0, waypointPositions.Length);
            controller.accumulator = Random.Range(0, idleDuration);
            controller.actor.RotateToward(controller.origin + waypointPositions[controller.waypointIndex]);
        }


        protected override void Stay(IController controller) 
        {
            Vector3 waypointPosition = controller.origin + waypointPositions[controller.waypointIndex];

            if (Vector3.Distance(waypointPosition, controller.position) > IWaypoint.distanceThreshold)
            {
                controller.animator.SetInteger("State", 2);
                controller.rigidbody2D.transform.position = Vector3.MoveTowards(controller.rigidbody2D.transform.position, waypointPosition, 4.1f * Time.deltaTime * 1);
            }
            else
            {
                controller.accumulator += Time.deltaTime;
                controller.animator.SetInteger("State", 0);

                if (controller.accumulator < idleDuration)
                    return;

                controller.accumulator = 0f;

                controller.waypointIndex++;
                controller.waypointIndex = controller.waypointIndex % waypointPositions.Length;

                controller.actor.RotateToward(controller.origin + waypointPositions[controller.waypointIndex]);
            }
        }

        protected override void Exit(IController controller) 
        {
        }
    }
}