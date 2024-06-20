using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.ControllerStates
{
    public class PlayerControls : MonoBehaviour, IPlayerControls, IController
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

        public IState.State actionState { get; set; }
        public IState.State goalState { get; set; }

        void Awake()
        {
            action = initialActionState;
            goal = initialGoalState;

        }

        void FixedUpdate()
        {
            if (AllieManager.Instance.count == 0)
                return;

            if (AllieManager.Instance.First().obj.GetComponent<Animator>().GetInteger("MovePriority") < int.MaxValue)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (GameStateManager.Instance.isPaused)
                return;

            actor = AllieManager.Instance.First();
            animator = actor.obj.GetComponent<Animator>();
            rigidbody2D = actor.obj.GetComponent<Rigidbody2D>();

            rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
            velocity = Vector3.zero;
        }

        void Update()
        {
            if (AllieManager.Instance.count == 0)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
                OptionsDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                EquipmentDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ScrollDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Tab) && BattleManager.Instance.aTBGuageFilledCount > 0)
                CommandDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                SwitchAllieDisplay.Instance.ToggleExclusivelyInParent();

            if (GameStateManager.Instance.isPaused)
                return;

            //switch between different allies
            if (CommandDisplay.Instance.gameObject.activeSelf == false && AllieManager.Instance.count > 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    AllieManager.Instance.CycleUp();
                else if (Input.GetKeyDown(KeyCode.Q))
                    AllieManager.Instance.CycleDown();
            }

            if (AllieManager.Instance.First().enabled == false)
                return;

            actor = AllieManager.Instance.First();
            animator = actor.obj.GetComponent<Animator>();
            rigidbody2D = actor.obj.GetComponent<Rigidbody2D>();

            action.UpdateState(this);
        }
    }
}