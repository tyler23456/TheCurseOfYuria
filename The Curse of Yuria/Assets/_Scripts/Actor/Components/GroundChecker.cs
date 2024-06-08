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

        public GroundChecker(GameObject obj)
        {
            animator = obj.transform.GetChild(0).GetComponent<Animator>();
            if (animator == null)
                animator = obj.transform.GetComponent<Animator>();
        }

        public void Update()
        {
            if (!Physics.Raycast(animator.transform.position, Vector2.down, 0.2f))
                return;

            isGrounded = Physics.SphereCast(animator.transform.position + Vector3.up * 0.55f, 0.5f, Vector3.down, out RaycastHit groundHit, 0.3f);
            animator.SetBool("IsGrounded", isGrounded);

            isFalling = Physics.SphereCast(animator.transform.position + Vector3.up * 0.55f, 0.4f, Vector3.down, out RaycastHit fallHit, 2f);
            animator.SetBool("IsFalling", !isFalling);
        }
    }
}