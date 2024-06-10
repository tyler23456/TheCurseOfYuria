using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.PlayerControls
{
    public class ClimbState : PlayerControllerStateBase
    {
        float gravityScale;
        IClimber trigger;

        protected override void Enter(PlayerControls controls)
        {
            controls.allie.animator.SetInteger("State", 5);
            controls.allie.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            gravityScale = controls.allie.rigidbody2D.gravityScale;
            controls.allie.rigidbody2D.gravityScale = 0f;
        }

        protected override void Stay(PlayerControls controls)
        {
            Ray ray = new Ray(AllieManager.Instance.First().obj.transform.position - Vector3.forward, Vector3.forward);
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
                controls.velocity += Vector2.up * controls.speed / 2f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                controls.velocity += Vector2.down * controls.speed / 2f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                controls.velocity += Vector2.left * controls.speed / 2f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                controls.velocity += Vector2.right * controls.speed / 2f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                controls.velocity += Vector2.up * 100;
                IPlayerControls.state = IPlayerControls.State.Normal;
            }
                
            if (trigger == null)
                IPlayerControls.state = IPlayerControls.State.Normal;
        }

        protected override void Exit(PlayerControls controls)
        {
            controls.allie.rigidbody2D.gravityScale = gravityScale;
        }
    }
}