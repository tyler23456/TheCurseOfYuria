using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.AStar
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class ControllerUnit : MonoBehaviour, IController, IPath
    {
        [SerializeField] TCOY.ControllerStates.ActionBase initialActionState;
        [SerializeField] TCOY.ControllerStates.GoalBase initialGoalState;
        [SerializeField] float _safeDistance = 30f;
        [SerializeField] float _battleDistance = 10f;
        [SerializeField] float _stopDistance = 2f;

        public Vector2 velocity { get; set; } = Vector2.zero;
        public float speed { get; set; } = 1f;
        public IActor actor { get; set; }
        public Animator animator { get; set; }
        public new Rigidbody2D rigidbody2D { get; set; }
        public bool pathSuccess { get; set; }
        public List<IWaypoint> waypoints { get; set; } = new List<IWaypoint>();
        public int index { get; set; }
        public Vector2 destination { get; set; }
        public Vector2 origin => transform.position;
        
        public float safeDistance => _safeDistance;
        public float battleDistance => _battleDistance;
        public float stopDistance => _stopDistance;

        public IAction action { get; set; }
        public IGoal goal { get; set; }
        public IState.State actionState { get; set; } = IState.State.enter;
        public IState.State goalState { get; set; } = IState.State.enter;

        void Awake()
        {
            actor = GetComponent<IActor>();
            animator = actor.obj.GetComponent<Animator>();
            rigidbody2D = actor.obj.GetComponent<Rigidbody2D>();
            action = initialActionState;
            goal = initialGoalState;
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

            action.UpdateState(this);
            goal.UpdateState(this);
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
            //action.OnDrawGizmosMethod(this);
        }
    }
}