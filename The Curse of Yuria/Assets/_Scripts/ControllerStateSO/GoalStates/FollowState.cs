using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.AStar;
using UnityEngine.Rendering;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "FollowState", menuName = "GoalStates/FollowState")]
    public class FollowState : GoalBase
    {
        protected override void Enter(IController controller)
        {
            base.Enter(controller);

            controller.actor.obj.transform.GetChild(0).GetComponent<SortingGroup>().sortingOrder = 500;

            if (controller.actor.obj.transform.parent.name == "Allies")
                controller.target = controller.actor.obj.transform.parent.GetChild(controller.actor.obj.transform.GetSiblingIndex() - 1).GetComponent<IPath>();    
            else
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

        IEnumerator CheckForPath(IController controller)
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