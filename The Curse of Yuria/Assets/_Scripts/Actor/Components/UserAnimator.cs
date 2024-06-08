using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.UserActors
{
    [System.Serializable]
    public class UserAnimator : IAnimator
    {
        [SerializeField] Animator animator;

        public bool isPerformingCommand => animator.GetBool("Action");
        public bool isGrounded => animator.GetBool("IsGrounded");

        public Action onStand = () => { };
        public Action onWalk = () => { };
        public Action onRun = () => { };
        public Action onJump = () => { };
        public Action onCrouch = () => { };
        public Action onClimb = () => { };
        public Action onKO = () => { };
        public Action onReady = () => { };
        public Action onUnready = () => { };
        public Action onUseSupply = () => { };
        public Action onCast = () => { };
        public Action onAttack = () => { };
        public Action onHit = () => { };
        public Action<int> onSetWeaponType = (type) => { };

        public UserAnimator(GameObject obj)
        {
            animator = obj.transform.GetChild(0).GetComponent<Animator>();
            if (animator == null)
                animator = obj.transform.GetComponent<Animator>();
        }

        void SetActionToTrue()
        {
            animator.SetBool("Action", true);
        }

        public void Stand()
        {
            onStand.Invoke();
        }

        public void Walk()
        {
            onWalk.Invoke();
        }

        public void Run()
        {
            onRun.Invoke();
        }

        public void Jump()
        {
            onJump.Invoke();
        }

        public void Crouch()
        {
            onCrouch.Invoke();
        }

        public void Climb()
        {
            onClimb.Invoke();
        }

        public void KO()
        {
            onKO.Invoke();
        }

        public void Ready()
        {
            onReady.Invoke();
        }

        public void Unready()
        {
            onReady.Invoke();
        }

        public void UseSupply()
        {
            onUseSupply.Invoke();
            SetActionToTrue();
        }  

        public void Attack()
        {
            onAttack.Invoke();
            SetActionToTrue();
        }

        public void Cast()
        {
            onCast.Invoke();
            SetActionToTrue();
        }

        public void Hit()
        {
            onHit.Invoke();
        }

        public void SetWeaponType(int type)
        {
            onSetWeaponType.Invoke(type);
        }
    }
}