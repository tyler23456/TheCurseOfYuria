using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.AStar
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class ControllerUnit : MonoBehaviour, IStateController
    {
        [SerializeField] TCOY.ControllerStates.StateBase initialState;
        [SerializeField] float _safeDistance = 30f;
        [SerializeField] float _battleDistance = 10f;
        [SerializeField] float _stopDistance = 2f;

        public Vector2 velocity { get; set; } = Vector2.zero;
        public float speed { get; set; } = 1f;
        public IActor actor { get; set; }
        public Animator animator { get; set; }
        public new Rigidbody2D rigidbody2D { get; set; }

        public float safeDistance => _safeDistance;
        public float battleDistance => _battleDistance;
        public float stopDistance => _stopDistance;

        public IState state { get; set; }

        void Awake()
        {
            actor = GetComponent<IActor>();
            animator = actor.obj.GetComponent<Animator>();
            rigidbody2D = actor.obj.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (AllieManager.Instance.count == 0)
                return;

            if (animator.GetInteger("MovePriority") < int.MaxValue)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (GameStateManager.Instance.isPaused)
                return;

            state.UpdateState(this);
        }

        void FixedUpdate()
        {
            if (AllieManager.Instance.count == 0)
                return;

            if (animator.GetInteger("MovePriority") < int.MaxValue)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (GameStateManager.Instance.isPaused)
                return;

            rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
            velocity = Vector2.zero;
        }
         
        void OnDrawGizmos()
        {
            state.OnDrawGizmosMethod(this);
        }
    }
}