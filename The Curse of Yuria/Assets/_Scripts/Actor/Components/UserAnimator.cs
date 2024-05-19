using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    [System.Serializable]
    public class UserAnimator : IAnimator
    {
        [SerializeField] Animator animator;

        public bool isPerformingCommand => animator.GetBool("Action");

        public UserAnimator(GameObject obj)
        {
            animator = obj.transform.GetChild(0).GetComponent<Animator>();
        }

        void SetActionToTrue()
        {
            animator.SetBool("Action", true);
        }

        public void Stand()
        {
            animator.SetInteger("State", 0);
        }

        public void Walk()
        {
            animator.SetInteger("State", 1);
        }

        public void Run()
        {
            animator.SetInteger("State", 2);
        }

        public void Jump()
        {
            animator.SetInteger("State", 3);
        }

        public void Crouch()
        {
            animator.SetInteger("State", 4);
        }

        public void Climb()
        {
            animator.SetInteger("State", 6);
        }

        public void KO()
        {
            animator.SetInteger("State", 7);
        }

        public void Ready()
        {
            animator.SetBool("Ready", true);
            animator.SetBool("Action", false);
        }

        public void UseSupply()
        {
            SetActionToTrue();
            animator.SetTrigger("UseSupply");
        }  

        public void Attack()
        {
            SetActionToTrue();
            animator.SetTrigger("Slash");
        }

        public void Cast()
        {
            SetActionToTrue();
            animator.SetTrigger("Cast");
        }

        public void Hit()
        {
            animator.SetTrigger("Hit");
        }

        public void SetWeaponType(int type)
        {
            animator.SetInteger("WeaponType", type);
        }
    }
}