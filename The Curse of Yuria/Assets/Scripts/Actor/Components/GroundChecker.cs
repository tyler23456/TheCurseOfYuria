using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    [System.Serializable]
    public class GroundChecker
    {
        [SerializeField] Animator animator;

        bool isGrounded;
        bool isFalling;

        public void Initialize()
        {

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