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
                controller.rigidbody2D.AddForce(Vector2.up * controller.speed * 25f * Time.deltaTime);
                stepSFX.UpdateOther(controller.audioSource, "LadderClimbStepSFX", minVolume, maxVolume, minPitch, maxPitch);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                controller.rigidbody2D.AddForce(Vector2.down * controller.speed * 25f * Time.deltaTime);
                stepSFX.UpdateOther(controller.audioSource, "LadderClimbStepSFX", minVolume, maxVolume, minPitch, maxPitch);
            }

            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                controller.SetAction(StateDatabase.Instance.GetAction("JumpState"));
            }
                
            if (trigger == null)
            {
                controller.SetAction(StateDatabase.Instance.GetAction("GroundState"));
                return;
            }

            Vector2 rigidbody2Dposition = controller.rigidbody2D.position;
            rigidbody2Dposition.x = Mathf.Lerp(rigidbody2Dposition.x, trigger.position.x, 4f * Time.deltaTime);
            controller.rigidbody2D.position = (rigidbody2Dposition);
        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("AutoPathState");
        }

        protected override void Exit(IController controller)
        {
            controller.rigidbody2D.gravityScale = gravityScale;
        }
    }
}