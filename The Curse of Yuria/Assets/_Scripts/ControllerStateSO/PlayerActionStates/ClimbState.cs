using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    [CreateAssetMenu(fileName = "ClimbState", menuName = "PlayerActionStates/ClimbState")]
    public class ClimbState : ActionBase
    {
        float gravityScale;
        IClimber trigger;
        Transform allies;

        protected override void Enter(IController controller)
        {
            controller.animator.SetInteger("State", 5);
            controller.actor.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            gravityScale = controller.rigidbody2D.gravityScale;
            controller.rigidbody2D.gravityScale = 0f;

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

                if (trigger != null && trigger.enabled == true)
                    break;
            }

            if (Input.GetKey(KeyCode.W))
            {
                controller.velocity += Vector2.up * controller.speed / 2f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                controller.velocity += Vector2.down * controller.speed / 2f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                controller.velocity += Vector2.left * controller.speed / 2f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                controller.velocity += Vector2.right * controller.speed / 2f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.velocity += Vector2.up * 100;
                IPlayerControls.state = IPlayerControls.State.Normal;
            }
                
            if (trigger == null)
                IPlayerControls.state = IPlayerControls.State.Normal;
        }

        protected override void Exit(IController controller)
        {
            controller.rigidbody2D.gravityScale = gravityScale;
        }
    }
}