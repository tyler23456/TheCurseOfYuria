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
            controllerStatesSFX.UpdateLandSFX(controller.audioSource);
        }

        protected void PlayerMovement(IController controller)
        {
            controller.animator.SetInteger("State", 0);

            if (Input.GetKey(KeyCode.A))
            {

                if (controller.rigidbody2D.transform.eulerAngles.y < 90f)
                    controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controller.animator.SetInteger("State", 2);
                    controller.rigidbody2D.AddForce(Vector2.left * controller.speed * 2f * 50f * Time.deltaTime);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                    IPlayerControls.isPlayerRunning = true;
                }
                else
                {
                    controller.animator.SetInteger("State", 1);
                    controller.rigidbody2D.AddForce(Vector2.left * controller.speed * 50f * Time.deltaTime);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                    IPlayerControls.isPlayerRunning = false;
                }
                IPlayerControls.hasPlayerMoved = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (controller.rigidbody2D.transform.eulerAngles.y > 90f)
                    controller.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controller.animator.SetInteger("State", 2);
                    controller.rigidbody2D.AddForce(Vector2.right * controller.speed * 2f * 50f * Time.deltaTime);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                    IPlayerControls.isPlayerRunning = true;
                }
                else
                {
                    controller.animator.SetInteger("State", 1);
                    controller.rigidbody2D.AddForce(Vector2.right * controller.speed * 50f * Time.deltaTime);
                    controllerStatesSFX.UpdateStepSFX(controller.audioSource);
                    IPlayerControls.isPlayerRunning = false;
                }
                IPlayerControls.hasPlayerMoved = true;
            }
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);

            PlayerMovement(controller);

            if (!controller.animator.GetBool("IsGrounded"))
                controller.SetAction(StateDatabase.Instance.GetAction("FallState"));

            else if (Input.GetKeyDown(KeyCode.Space) && controller.animator.GetBool("IsGrounded"))
                controller.SetAction(StateDatabase.Instance.GetAction("JumpState"));
        }

        protected override void Exit(IController controller)
        {

        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("AutoGroundState");
        }
    }
}
