using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.ControllerStates
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        [SerializeField] TCOY.ControllerStates.ActionBase initialActionState;
        [SerializeField] TCOY.ControllerStates.GoalBase initialGoalState;


        void Awake()
        {
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
                ItemDisplay.Instance.ToggleExclusivelyInParent();

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
        }
    }
}