using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.UserActors
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        float speed = 1f;
        IAllie allie;
        Vector2 velocity = Vector2.zero;

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
            allie.animator.SetInteger("State", 0);

            if (Input.GetKey(KeyCode.A))
            {
                allie.obj.transform.GetChild(0).eulerAngles = new Vector3(0f, 180f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    allie.animator.SetInteger("State", 2);
                    velocity += Vector2.left * speed * 2f;
                }
                else
                {
                    allie.animator.SetInteger("State", 1);
                    velocity += Vector2.left * speed;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                allie.obj.transform.GetChild(0).eulerAngles = new Vector3(0f, 0f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    allie.animator.SetInteger("State", 2);
                    velocity += Vector2.right * speed * 2f;
                }
                else
                {
                    allie.animator.SetInteger("State", 1);
                    velocity += Vector2.right * speed;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
                velocity += Vector2.up * 100;
        }
    }
}