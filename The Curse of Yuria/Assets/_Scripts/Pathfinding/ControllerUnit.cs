using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace TCOY.Pathfinding
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class ControllerUnit : MonoBehaviour, IController, IPath
    {
        public bool isActive { get; protected set; } = true;

        [SerializeField] GoalState initialGoalState;
        [SerializeField] ActionState initialActionState;

        Transform allies;

        public float accumulator { get; set; } = 0f;

        public Vector2 origin { get; private set; }
        public Vector2 velocity { get; set; } = Vector2.zero;
        public float speed { get; set; } = 28f * 2f;
        public IActor actor { get; set; }
        public Animator animator { get; set; }
        public new Rigidbody2D rigidbody2D { get; set; }
        public AudioSource audioSource { get; set; }
        public bool pathSuccess { get; set; }

        public IWaypoint previousWaypoint { get; set; }
        public List<SimpleWaypoint> waypoints { get; set; } = new List<SimpleWaypoint>();
        public int waypointIndex { get; set; }
        public Vector2[] subWaypoints { get; set; } = new Vector2[2];
        public int subWaypointIndex { get; set; } = 0;
        public IPath target { get; set; }
        public Vector2 position => transform.position;
        public IConnection connection { get; set; }
        public IConnection pathfindingConnection { get; set; }
        public Vector2 contactPoint { get; private set; }

        public GoalState goal { get; set; }
        public ActionState action { get; set; }

        public GoalState.State goalState { get; set; } = GoalState.State.enter;
        public ActionState.State actionState { get; set; } = ActionState.State.enter;

        public bool isPathfindingPaused { get; set; } = false;
        public bool isAutoMovementPaused { get; set; } = false;

        public bool previousIsGrounded { get; private set; } = false;
        public bool isGrounded { get; private set; } = false;
        public bool forcePathReconnection { get; set; } = false;

        public void ResetToDefault()
        {
            forcePathReconnection = true;
            SetGoal(initialGoalState);
        }

        public void Activate()
        {
            this.isActive = true;
        }

        public void Deactivate()
        {
            this.isActive = false;
        }

        void Awake()
        {
            actor = GetComponent<IActor>();
            animator = actor.obj.GetComponent<Animator>();
            rigidbody2D = actor.obj.GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();

            if (goal == null)
                goal = initialGoalState;
            if (action == null)
                action = initialActionState;

            origin = transform.position;
        }

        void Start()
        {
            if (allies == null)
                allies = GameObject.Find("/DontDestroyOnLoad/Allies").transform;
        }

        public void SetGoal(GoalState goal)
        {
            if (this.goal != null)
            {
                goalState = GoalState.State.exit;
                this.goal.UpdateState(this);
            }
            this.goal = goal;
        }

        public void SetAction(ActionState action)
        {
            if (this.action != null)
            {
                actionState = ActionState.State.exit;
                this.action.UpdateState(this);
            }     
            this.action = action;
        }

        public void SetInitialStates(GoalState initialGoalState, ActionState initialActionState)
        {
            this.initialGoalState = initialGoalState;
            this.initialActionState = initialActionState;
        }

        void Update()
        {
            if (!isActive)
                return;

            if (actor.hasKOStatusEffect)
                return;

            if (allies.childCount == 0)
                return;

            if (animator.GetInteger("MovePriority") < int.MaxValue)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (GameStateManager.Instance.isWaiting)
                return;

            if (GameStateManager.Instance.isPaused)
                return;

            goal.UpdateState(this);
            action.UpdateState(this);
        }

        void FixedUpdate()
        {
            previousIsGrounded = isGrounded;
            isGrounded = false;

            if (!isActive)
                return;

            if (actor.hasKOStatusEffect)
                return;

            if (allies.childCount == 0)
                return;

            if (animator.GetInteger("MovePriority") < int.MaxValue)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (GameStateManager.Instance.isWaiting)
                return;

            if (GameStateManager.Instance.isPaused)
                return;

            action.FixedUpdateState(this);//
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            Connection connection = collision.GetComponent<Connection>();

            if (connection == null || connection.getAction.name == "AutoJumpState")
                return;
            
            this.connection = connection;
            contactPoint = collision.bounds.ClosestPoint(transform.position);
            
            isGrounded = true;
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (name != "Nate")
                return;

            List<Vector2> points = new List<Vector2>();
            points.Add(transform.position);
            foreach(SimpleWaypoint waypoint in waypoints)
            {
                points.Add(waypoint.position);
            }

            for (int i = 1; i < points.Count; i++)
            {
                var p1 = points[i - 1];
                var p2 = points[i];
                var thickness = 10;
                Handles.DrawBezier(p1, p2, p1, p2, Color.red, null, thickness);
            }

            //groundChecker?.OnDrawGizmos();
        }
#endif
    }
}