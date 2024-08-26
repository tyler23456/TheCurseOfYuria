using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Pathfinding;
using UnityEngine.Rendering;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "FollowState", menuName = "GoalStates/FollowState")]
    public class FollowState : GoalBase
    {
        static Transform parent;

        protected override void Enter(IController controller)
        {
            base.Enter(controller);

            parent = controller.actor.obj.transform.parent;

            if (parent.name != "Allies")
                controller.target = GameObject.Find("/DontDestroyOnLoad/Allies").transform.GetChild(0).GetComponent<IPath>();         

            controller.SetAction(StateDatabase.Instance.GetAction("AutoResetState"));
            controller.waypoints.Clear();
            controller.waypointIndex = 0;
            controller.StartCoroutine(CheckForPath(controller));
        }

        
        protected override void Exit(IController controller)
        {
            base.Exit(controller);
            controller.StopAllCoroutines();
        }

        public override void OnDrawGizmosMethod(IController controller)
        {
            if (controller.waypoints != null)
            {
                for (int i = controller.waypointIndex; i < controller.waypoints.Count; i++)
                {
                    Gizmos.color = Color.red / 2f;
                    Gizmos.DrawCube(controller.waypoints[i].position, Vector3.one);

                    if (i == controller.waypointIndex)
                        Gizmos.DrawLine(controller.rigidbody2D.transform.position, controller.waypoints[i].position);
                    else
                        Gizmos.DrawLine(controller.waypoints[i - 1].position, controller.waypoints[i].position);
                }
            }
        }
    }
}