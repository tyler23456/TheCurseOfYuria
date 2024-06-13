using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.PlayerControls
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        public float speed { get; set; } = 1f;
        public IAllie allie { get; set; }
        public Vector2 velocity { get; set; } = Vector2.zero;

        PlayerControllerStateBase stateObject = new NormalState();
        IPlayerControls.State currentState = IPlayerControls.State.Normal;

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

            allie = AllieManager.Instance.First();
            
            allie.rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
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

            if (Input.GetKeyDown(KeyCode.Tab) && BattleManager.Instance.aTBGuageFilledQueue.Count > 0)
                CommandDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                SwitchAllieDisplay.Instance.ToggleExclusivelyInParent();

            if (GameStateManager.Instance.isPaused)
                return;

            //switch between different allies
            if (CommandDisplay.Instance.gameObject.activeSelf == false && AllieManager.Instance.count > 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    AllieManager.Instance.MoveIndex(0, AllieManager.Instance.GetSafeSelectedIndex(2));
                else if (Input.GetKeyDown(KeyCode.Q))
                    AllieManager.Instance.MoveIndex(AllieManager.Instance.GetSafeSelectedIndex(2), 0);
            }

            allie = AllieManager.Instance.First();

            ExecuteControllerState();
        }

        void ExecuteControllerState()
        {
            if (IPlayerControls.state != currentState)
            {
                stateObject.MarkStateAsFinished(this);

                switch (IPlayerControls.state)
                {
                    case IPlayerControls.State.Normal:
                        stateObject = new NormalState();
                        currentState = IPlayerControls.State.Normal;
                        break;
                    case IPlayerControls.State.Combat:
                        stateObject = new CombatState();
                        currentState = IPlayerControls.State.Combat;
                        break;
                    case IPlayerControls.State.Climb:
                        stateObject = new ClimbState();
                        currentState = IPlayerControls.State.Climb;
                        break;
                }
            }

            stateObject.Update(this);
        }
    }
}