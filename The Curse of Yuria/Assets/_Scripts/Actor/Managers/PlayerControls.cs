using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine.Rendering;

namespace TCOY.UserActors
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        [SerializeField] Transform optionsDisplay;
        [SerializeField] Transform itemsDisplay;
        [SerializeField] Transform commandDisplay;
        [SerializeField] Transform switchAllieDisplay;

        [SerializeField] Camera mainCamera;

        [SerializeField] GoalState selectedDefaultGoal;
        [SerializeField] GoalState unselectedDefaultGoal;

        IController controller;
        int defaultMainCameraLayerMask = 0;

        void Awake()
        {
            defaultMainCameraLayerMask = mainCamera.cullingMask;
        }

        void OnEnable()
        {
            OnTransformChildrenChanged();
        }

        public void Refresh()
        {
            OnTransformChildrenChanged();
        }

        public void SetUnselectedDefaultGoal(GoalState goal)
        {
            this.unselectedDefaultGoal = goal;
        }

        public void SetUnselectedGoal(GoalState goal)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                controller = transform.GetChild(i).GetComponent<IController>();
                controller.SetGoal(goal);
            }
        }

        void Update()
        {
            if (transform.childCount == 0)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (GameStateManager.Instance.isWaiting)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
                optionsDisplay.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
                itemsDisplay.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Tab) && IBattleData.aTBGuagesFilled.Count > 0)
                commandDisplay.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                switchAllieDisplay.gameObject.SetActive(true);

            if (GameStateManager.Instance.isPaused)
                return;

            if (commandDisplay.gameObject.activeSelf == false && transform.childCount > 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    RotateActiveAllies(true);
                else if (Input.GetKeyDown(KeyCode.Q))
                    RotateActiveAllies(false);
            }
        }

        void RotateActiveAllies(bool isRotatingClockwise)
        {
            int count = Mathf.Min(transform.childCount, IAllie.MaxActiveAlliesCount);

            for (int i = 0; i < count ; i++)
            {
                if (isRotatingClockwise)
                    transform.GetChild(0).SetSiblingIndex(count - 1);
                else
                    transform.GetChild(count - 1).SetSiblingIndex(0);
                
                if (transform.GetChild(0).GetComponent<IActor>().enabled == true)
                    break;
            }
        }

        void OnTransformChildrenChanged()
        {
            if (transform.childCount == 0)
                return;

            mainCamera.cullingMask = defaultMainCameraLayerMask;

            int count = Mathf.Min(transform.childCount, IAllie.MaxActiveAlliesCount);

            controller = transform.GetChild(0).GetComponent<IController>();

            if (IPlayerControls.initializeGoalStatesOnRefresh)
                controller.SetGoal(selectedDefaultGoal);

            for (int i = 1; i < transform.childCount; i++)
            {
                controller = transform.GetChild(i).GetComponent<IController>();

                if (IPlayerControls.initializeGoalStatesOnRefresh)
                    controller.ResetToDefault();
            }

            for (int i = 0; i < count; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetChild(0).GetComponent<SortingGroup>().sortingOrder = 510 - (i * 3);
                mainCamera.cullingMask |= (1 << transform.GetChild(i).GetChild(0).gameObject.layer);
            }
            
            for (int i = IAllie.MaxActiveAlliesCount; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            IPlayerControls.initializeGoalStatesOnRefresh = true;
        }

        private void OnDrawGizmos()
        {
            if (IBattleData.isInBattle)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.blue;

            Gizmos.DrawSphere(transform.GetChild(0).position, 1f);

        }
    }
}