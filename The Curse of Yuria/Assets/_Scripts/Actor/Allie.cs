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

        new protected void Awake()
        {
            base.Awake();

            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            aTBGuage.OnATBGuageFilled = () => BattleManager.Instance.AddATBGuageFilled(this);

            stats.onHPDamage += (damage) => animator.SetTrigger("Hit");
        }

        new protected void Update()
        {
            base.Update();
        }
    }
}