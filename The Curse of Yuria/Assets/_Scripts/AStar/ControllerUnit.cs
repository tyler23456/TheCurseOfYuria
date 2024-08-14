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
        [SerializeField] GoalState initialGoalState;
        [SerializeField] ActionState initialActionState;
        [SerializeField] float _safeDistance = 30f;
        [SerializeField] float _battleDistance = 10f;
        [SerializeField] float _stopDistance = 2f;
        [SerializeField] float _goDistance = 3f;

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

        public float safeDistance => _safeDistance;
        public float battleDistance => _battleDistance;
        public float stopDistance { get { return _stopDistance; } set { _stopDistance = value; } }
        public float goDistance { get { return _goDistance; } set { _goDistance = value; } }

        public GoalState goal { get; set; }
        public ActionState action { get; set; }

        public GoalState.State goalState { get; set; } = GoalState.State.enter;
        public ActionState.State actionState { get; set; } = ActionState.State.enter;

        public bool isGroundedEnter => groundChecker.isGroundedEnter;
        public bool isGroundedExit => groundChecker.isGroundedExit;

        protected GroundChecker groundChecker;
        public bool isTouchingTargetableConnection { get; private set; }
        public bool isPathfindingPaused { get; set; } = false;
        public bool isInitialized { get; set; } = false;
        public bool isAutoMovementPaused { get; set; } = false;
        public int idleState { get; set; } = 0;

        public void ResetToDefault()
        {
            waypointIndex = 0;
            subWaypointIndex = 0;
            waypoints.Clear();

            SetGoal(StateDatabase.Instance.GetGoal("FollowState"));
            SetAction(StateDatabase.Instance.GetAction("AutoGroundState"));

            isInitialized = false;
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

            groundChecker = new GroundChecker(animator);
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
            if (allies.childCount == 0)
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
            isTouchingTargetableConnection = false;

            if (allies.childCount == 0)
                return;

            if (animator.GetInteger("MovePriority") < int.MaxValue)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (GameStateManager.Instance.isPaused)
                return;

            action.FixedUpdateState(this);
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            Connection connection = collision.GetComponent<Connection>();

            if (connection == null || connection.getAction.name == "AutoJumpState")
                return;

            if (!isInitialized)
            {
                pathfindingConnection = connection;
                isInitialized = true;
            }
            
            this.connection = connection;
            contactPoint = collision.bounds.ClosestPoint(transform.position);
            isTouchingTargetableConnection = true;
        }
        
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
    }
}