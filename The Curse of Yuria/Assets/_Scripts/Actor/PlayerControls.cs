using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.ControllerStates
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        [SerializeField] Transform aTBGuagesFilled;

        [SerializeField] GoalSO selectedDefaultGoal;
        [SerializeField] GoalSO unselectedDefaultGoal;
        [SerializeField] ActionSO selectedDefaultAction;
        [SerializeField] ActionSO unselectedDefaultAction;

        void Awake()
        {
        }

        void Start()
        {
            OnTransformChildrenChanged();
        }


        void Update()
        {
            if (transform.childCount == 0)
                return;

            if (GameStateManager.Instance.isStopped)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
                OptionsDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                ItemDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Tab) && aTBGuagesFilled.childCount > 0)
                CommandDisplay.Instance.ToggleExclusivelyInParent();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                SwitchAllieDisplay.Instance.ToggleExclusivelyInParent();

            if (GameStateManager.Instance.isPaused)
                return;

            if (CommandDisplay.Instance.gameObject.activeSelf == false && transform.childCount > 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    RotateActiveAllies(true);
                else if (Input.GetKeyDown(KeyCode.Q))
                    RotateActiveAllies(false);
            }
        }

        void RotateActiveAllies(bool isRotatingClockwise)
        {
            int count = Mathf.Min(transform.childCount,IAllie.MaxActiveAlliesCount);

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
            int count = Mathf.Min(transform.childCount, IAllie.MaxActiveAlliesCount);

            IController firstController = transform.GetChild(0).GetComponent<IController>();
            firstController.SetGoal(selectedDefaultGoal);
            firstController.SetAction(selectedDefaultAction); //temporary
            Debug.Log("0  " + firstController.goal.name + "   " + firstController.action.name);

            for (int i = 0; i < count; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
                
            for (int i = IAllie.MaxActiveAlliesCount; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).position = Vector3.down * 10000;
            }

            for (int i = 1; i < transform.childCount; i++)
            {
                IController controller = transform.GetChild(i).GetComponent<IController>();
                controller.SetGoal(unselectedDefaultGoal);
                controller.SetAction(unselectedDefaultAction); //temporary
                Debug.Log(i.ToString() + "  " + controller.goal.name + "   " + controller.action.name);
            }
                
        }
    }
}