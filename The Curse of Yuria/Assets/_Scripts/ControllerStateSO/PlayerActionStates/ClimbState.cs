using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "ClimbState", menuName = "PlayerActionStates/ClimbState")]
    public class ClimbState : ActionBase
    {
        const float minVolume = 0.7f;
        const float maxVolume = 1f;
        const float minPitch = 0.7f;
        const float maxPitch = 1.3f;

        float gravityScale;
        IClimber trigger;
        Transform allies;

        ControllerStatesSFX stepSFX = new ControllerStatesSFX();

        protected override void Enter(IController controller)
        {
            controller.animator.SetInteger("State", 5);
            controller.actor.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            gravityScale = controller.rigidbody2D.gravityScale;
            controller.rigidbody2D.gravityScale = 0f;

            ControllerStatesSFXManager.Instance.PlayStepSFX("LandWoodStepSFX", controller.audioSource, 0.1f, 0.2f, 0.7f, 1.3f);

            if (allies == null)
                allies = GameObject.Find("/DontDestroyOnLoad/Allies").transform;
        }

        protected override void Stay(IController controller)
        {
            Vector3 position = allies.GetChild(0).position;

            Ray ray = new Ray(position - Vector3.forward, Vector3.forward);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            trigger = null;
            foreach (RaycastHit2D hit in hits)
            {
                trigger = hit.transform.GetComponent<IClimber>();

                if (trigger != null)
                    break;
            }

            if (Input.GetKey(KeyCode.W))
            {
                controller.animator.SetInteger("State", 5);
                controller.rigidbody2D.AddForce(Vector2.up * controller.speed / 2f);
                stepSFX.UpdateOther(controller.audioSource, "LadderClimbStepSFX", minVolume, maxVolume, minPitch, maxPitch);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                controller.animator.SetInteger("State", 5);
                controller.rigidbody2D.AddForce(Vector2.down * controller.speed / 2f);
                stepSFX.UpdateOther(controller.audioSource, "LadderClimbStepSFX", minVolume, maxVolume, minPitch, maxPitch);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                controller.animator.SetInteger("State", 5);
                controller.rigidbody2D.AddForce(Vector2.left * controller.speed / 2f);
                stepSFX.UpdateOther(controller.audioSource, "LadderClimbStepSFX", minVolume, maxVolume, minPitch, maxPitch);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                controller.animator.SetInteger("State", 5);
                controller.rigidbody2D.AddForce(Vector2.right * controller.speed / 2f);
                stepSFX.UpdateOther(controller.audioSource, "LadderClimbStepSFX", minVolume, maxVolume, minPitch, maxPitch);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.SetAction(StateDatabase.Instance.GetAction("JumpState"));
            }
                
            if (trigger == null)
                controller.SetAction(StateDatabase.Instance.GetAction("GroundState"));
        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("AutoClimbState");
        }

        protected override void Exit(IController controller)
        {
            controller.rigidbody2D.gravityScale = gravityScale;
        }
    }
}