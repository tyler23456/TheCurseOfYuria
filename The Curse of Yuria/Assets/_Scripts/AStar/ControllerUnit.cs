using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

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
        public float speed { get; set; } = 28f;
        public IActor actor { get; set; }
        public Animator animator { get; set; }
        public new Rigidbody2D rigidbody2D { get; set; }
        public bool pathSuccess { get; set; }
        public List<Vector2> waypoints { get; set; } = new List<Vector2>();
        public int index { get; set; }
        public Vector2 destination { get; set; }
        public Vector2 position => transform.position;
        public IConnection connection { get; private set; }
        
        public float safeDistance => _safeDistance;
        public float battleDistance => _battleDistance;
        public float stopDistance => _stopDistance;

        public IAction action { get; set; }
        public IGoal goal { get; set; }
        public IState.State actionState { get; set; } = IState.State.enter;
        public IState.State goalState { get; set; } = IState.State.enter;

        protected TCOY.UserActors.GroundChecker groundChecker;

        void Awake()
        {
            actor = GetComponent<IActor>();
            animator = actor.obj.GetComponent<Animator>();
            rigidbody2D = actor.obj.GetComponent<Rigidbody2D>();
            action = initialActionState;
            goal = initialGoalState;

            groundChecker = new UserActors.GroundChecker(animator);
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

            groundChecker.Update();
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
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            IConnection connection = collision.GetComponent<IConnection>();

            if (connection == null)
                return;

            this.connection = connection;
        }

        void OnDrawGizmos()
        {
            if (name != "Chicken")
                return;

            List<Vector2> points = new List<Vector2>();
            points.Add(transform.position);
            foreach(Vector2 position in waypoints)
            {
                points.Add(position);
            }

            for (int i = 1; i < points.Count; i++)
            {
                var p1 = points[i - 1];
                var p2 = points[i];
                var thickness = 6;
                Handles.DrawBezier(p1, p2, p1, p2, Color.red, null, thickness);
            }

            groundChecker?.OnDrawGizmos();
        }
    }
}