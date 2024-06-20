using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "GroundState", menuName = "PlayerActionStates/GroundState")]
    public class GroundState : ActionBase
    {
        protected override void Enter(IController controller)
        {

        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);

            controller.animator.SetInteger("State", 0);

            if (Input.GetKeyDown(KeyCode.A))
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0, 0f);
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controller.animator.SetInteger("State", 2);
                    controller.velocity += Vector2.left * controller.speed * 2f;
                }
                else
                {
                    controller.animator.SetInteger("State", 1);
                    controller.velocity += Vector2.left * controller.speed;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controller.animator.SetInteger("State", 2);
                    controller.velocity += Vector2.right * controller.speed * 2f;
                }
                else
                {
                    controller.animator.SetInteger("State", 1);
                    controller.velocity += Vector2.right * controller.speed;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.action = StateDatabase.Instance.GetAction("JumpState");
                controller.actionState = IState.State.exit;
            }
                
        }

        protected override void Exit(IController controller)
        {

        }
    }
}
