using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "AutoFallState", menuName = "AutoActionStates/AutoFallState")]
    public class AutoFallState : ActionBase
    {
        protected override void Enter(IController controller)
        {
            //has AI fall until they are touching a targetable connection.
            //Then it will force
            controller.rigidbody2D.isKinematic = false;
            controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);

            //controller.rigidbody2D.transform.position += Vector3.down * 10f * Time.deltaTime;

            if (controller.isGrounded)
            {
                controller.SetAction(controller.connection.getAction);
                controller.pathfindingConnection = controller.connection;
                controller.actor.obj.SetActive(false);
                controller.actor.obj.SetActive(true);
                controller.StopAllCoroutines();
                Pathfinding.PathRequester.RequestPath(controller, controller.target);
                controller.StartCoroutine(controller.goal.CheckForPath(controller));
            }
                
        }

        protected override void Exit(IController controller)
        {
            controller.rigidbody2D.isKinematic = true;
            controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("FallState"); ;
        }
    }
}


