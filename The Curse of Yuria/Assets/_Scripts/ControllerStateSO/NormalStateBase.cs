using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public abstract class NormalStateBase : StateBase
    {
        protected override void Enter(IStateController controller)
        {

        }

        protected override void Stay(IStateController controller)
        {
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
                controller.velocity += Vector2.up * 100;
        }

        protected override void Exit(IStateController controller)
        {

        }
    }
}
