using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Allie : Actor, IAllie, IActor
    {
        public new Rigidbody2D rigidbody2D { get; private set; }
        public Animator animator { get; private set; }

        GroundChecker groundChecker;

        new protected void Awake()
        {
            base.Awake();

            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            groundChecker = new GroundChecker(animator);
            
            aTBGuage.OnATBGuageFilled = () => IBattleData.aTBGuagesFilled.AddLast(this);

            stats.onHPDamage += (damage) => animator.SetTrigger("Hit");

            stats.onZeroHealth += () => transform.parent.GetComponent<IPlayerControls>().ResetTargets();
        }

        protected void FixedUpdate()
        {
            groundChecker.Update();
        }

        new protected void Update()
        {
            base.Update();
        }

        
    }
}