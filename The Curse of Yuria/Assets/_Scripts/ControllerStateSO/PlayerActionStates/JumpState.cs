using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "JumpState", menuName = "PlayerActionStates/JumpState")]
    public class JumpState : GroundState
    {
        const float MaxDuration = 0.25f;

        protected override void Enter(IController controller)
        {
            base.Enter(controller);
            controller.accumulator = 0f;
            controller.rigidbody2D.gravityScale = 0f;

            controller.animator.SetInteger("State", 2);
        }

        protected override void Stay(IController controller)
        {
            base.Stay(controller);

            controller.accumulator += Time.deltaTime;

            controller.rigidbody2D.AddForce(Vector2.up * 0.25f * controller.speed, ForceMode2D.Impulse);

            if (Input.GetKey(KeyCode.Space) && controller.accumulator < MaxDuration)
                return;

            controller.SetAction(StateDatabase.Instance.GetAction("GroundState"));
        }

        protected override void Exit(IController controller)
        {
            base.Exit(controller);

            controller.rigidbody2D.gravityScale = 1f;
        }

        public override ActionState GetSisterState()
        {
            return StateDatabase.Instance.GetAction("AutoJumpState");
        }
    }
}