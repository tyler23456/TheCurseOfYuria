using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "GroundState", menuName = "PlayerActionStates/GroundState")]
    public class GroundState : ActionBase
    {
        ControllerStatesSFX controllerStatesSFX = new ControllerStatesSFX();

        protected override void Enter(IController controller)
        {
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);

            controller.animator.SetInteger("State", 0);




            if (controller.isGroundedEnter)
            {
                controllerStatesSFX.UpdateLandSFX(controller.audioSource);
            }
            else if (Input.GetKey(KeyCode.A))
            {

                if (controller.rigidbody2D.transform.eulerAngles.y < 90f)
                    controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controller.animator.SetInteger("State", 2);
                    controller.rigidbody2D.AddForce(Vector2.left * controller.speed * 2f);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                }
                else
                {
                    controller.animator.SetInteger("State", 1);
                    controller.rigidbody2D.AddForce(Vector2.left * controller.speed);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (controller.rigidbody2D.transform.eulerAngles.y > 90f)
                    controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controller.animator.SetInteger("State", 2);
                    controller.rigidbody2D.AddForce(Vector2.right * controller.speed * 2f);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                }
                else
                {
                    controller.animator.SetInteger("State", 1);
                    controller.rigidbody2D.AddForce(Vector2.right * controller.speed);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
                controller.SetAction(StateDatabase.Instance.GetAction("JumpState"));
        }

        protected override void Exit(IController controller)
        {

        }
    }
}
