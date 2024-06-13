using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.PlayerControls
{
    public class NormalStateBase : PlayerControllerStateBase
    {
        protected override void Enter(PlayerControls controls)
        {

        }

        protected override void Stay(PlayerControls controls)
        {
            controls.allie.animator.SetInteger("State", 0);

            if (Input.GetKeyDown(KeyCode.A))
            {
                controls.allie.rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                controls.allie.rigidbody2D.transform.eulerAngles = new Vector3(0f, 0, 0f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controls.allie.animator.SetInteger("State", 2);
                    controls.velocity += Vector2.left * controls.speed * 2f;
                }
                else
                {
                    controls.allie.animator.SetInteger("State", 1);
                    controls.velocity += Vector2.left * controls.speed;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controls.allie.animator.SetInteger("State", 2);
                    controls.velocity += Vector2.right * controls.speed * 2f;
                }
                else
                {
                    controls.allie.animator.SetInteger("State", 1);
                    controls.velocity += Vector2.right * controls.speed;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
                controls.velocity += Vector2.up * 100;
        }

        protected override void Exit(PlayerControls controls)
        {

        }
    }
}
