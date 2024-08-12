using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.ControllerStates
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        [SerializeField] Transform optionsDisplay;
        [SerializeField] Transform itemsDisplay;
        [SerializeField] Transform commandDisplay;
        [SerializeField] Transform switchAllieDisplay;

        [SerializeField] GoalState selectedDefaultGoal;
        [SerializeField] GoalState unselectedDefaultGoal;
        [SerializeField] ActionState selectedDefaultAction;
        [SerializeField] ActionState unselectedDefaultAction;

        float[] stopDistances = new float[3] { 0f, 1.5f, 3f };
        float[] goDistances = new float[3] { 0.75f, 2.25f, 3.75f }; 
        IController controller;

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
                optionsDisplay.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
                itemsDisplay.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Tab) && IBattleData.aTBGuagesFilled.Count > 0)
                commandDisplay.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                switchAllieDisplay.gameObject.SetActive(true);

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
            if (transform.childCount == 0)
                return;

            int count = Mathf.Min(transform.childCount, IAllie.MaxActiveAlliesCount);

            controller = transform.GetChild(0).GetComponent<IController>();
            controller.rigidbody2D.isKinematic = false;
            controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            controller.SetGoal(selectedDefaultGoal);
            controller.SetAction(selectedDefaultAction);

            for (int i = 0; i < count; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);

                controller = transform.GetChild(i).GetComponent<IController>();
                controller.stopDistance = stopDistances[i];
                controller.goDistance = goDistances[i];

            }
            
            for (int i = IAllie.MaxActiveAlliesCount; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 1; i < transform.childCount; i++)
            {
                controller = transform.GetChild(i).GetComponent<IController>();
                controller.SetGoal(unselectedDefaultGoal);
                controller.SetAction(unselectedDefaultAction); //temporary
                controller.rigidbody2D.isKinematic = true;
                controller.rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                controller.rigidbody2D.velocity = Vector2.zero;
                Debug.Log(i.ToString() + "  " + controller.goal.name + "   " + controller.action.name);
            }
                
        }
    }
}