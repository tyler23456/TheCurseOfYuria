using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    [System.Serializable]
    public class GroundChecker
    {
        Animator animator;

        bool isGrounded;
        bool isFalling;

        public GroundChecker(Animator animator)
        {
            this.animator = animator;
        }

        public void Update()
        {
            //if (!Physics2D.Raycast(animator.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("TileCollision")))
                //return;

            isGrounded = Physics2D.CircleCast(animator.transform.position, 0.3f, Vector3.down, 0.3f, LayerMask.GetMask("TileCollision"));
            animator.SetBool("IsGrounded", isGrounded);

            //isFalling = Physics2D.CircleCast(animator.transform.position, 0.4f, Vector3.down, 1f, LayerMask.GetMask("TileCollision"));
            //animator.SetBool("IsFalling", !isFalling);
        }

        public void OnDrawGizmos()
        {
            if (animator == null)
                return;

            if (isGrounded)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(animator.transform.position, 0.3f);
        }
    }
}