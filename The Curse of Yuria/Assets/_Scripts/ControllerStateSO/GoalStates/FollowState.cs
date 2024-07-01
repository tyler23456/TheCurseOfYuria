using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.AStar;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "FollowState", menuName = "GoalStates/FollowState")]
    public class FollowState : GoalBase
    {
        Transform allies;

        public override bool CheckForTransition(IController controller)
        {
            return Vector3.Distance(controller.position, controller.destination) < controller.battleDistance;
        }

        protected override void Enter(IController controller)
        {
            base.Enter(controller);

            if (allies == null)
                allies = GameObject.Find("/DontDestroyOnLoad/Allies").transform;

            controller.destination = allies.GetChild(0).position;
            controller.waypoints.Clear();
            controller.actor.StartCoroutine(CheckForPath(controller));
        }

       
        protected override void Exit(IController controller)
        {
            base.Exit(controller);
            controller.actor.StopCoroutine(CheckForPath(controller));
        }

        IEnumerator CheckForPath(IController controller)
        {
            while (true)
            {
                if (controller.animator.GetInteger("MovePriority") < int.MaxValue) //|| controller.actor.enabled == false)
                    yield return null;

                controller.destination = allies.GetChild(0).position;
                PathRequester.RequestPath(controller, allies.GetChild(0).GetComponent<IPath>());

                yield return new WaitForSeconds(0.2f);
            }
        }

        public override void OnDrawGizmosMethod(IController controller)
        {
            if (controller.waypoints != null)
            {
                for (int i = controller.index; i < controller.waypoints.Count; i++)
                {
                    Gizmos.color = Color.red / 2f;
                    Gizmos.DrawCube(controller.waypoints[i], Vector3.one);

                    if (i == controller.index)
                        Gizmos.DrawLine(controller.rigidbody2D.transform.position, controller.waypoints[i]);
                    else
                        Gizmos.DrawLine(controller.waypoints[i - 1], controller.waypoints[i]);
                }
            }
        }
    }
}