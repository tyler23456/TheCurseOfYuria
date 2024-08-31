using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoResetState", menuName = "AutoActionStates/AutoResetState")]
    public class AutoResetState : ActionBase
    { 
        protected override void Enter(IController controller)
        {
            //automatically checks path and has AI fall to a connection if no connection exists.
            if (controller.isGrounded && controller.connection != null && !controller.forcePathReconnection)
            {
                controller.SetAction(controller.connection.getAction);
                controller.pathfindingConnection = controller.connection;
                controller.rigidbody2D.Sleep();
                controller.rigidbody2D.WakeUp();
                controller.StopAllCoroutines();
                controller.waypointIndex = 0;
                controller.waypoints.Clear();
                Pathfinding.PathRequester.RequestPath(controller, controller.target);
                controller.StartCoroutine(controller.goal.CheckForPath(controller));
            }    
            else
            {
                controller.SetAction(StateDatabase.Instance.GetAction("AutoFallState"));
                controller.forcePathReconnection = false;
            }
                
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);
        }

        protected override void Exit(IController controller)
        {

        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("ResetState");
        }
    }
}
