using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.UserActors
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        float speed = 1f;
        Actor actor;
        Vector2 velocity = Vector2.zero;
        
        void Start()
        {
            actor = GetComponent<Actor>();
        }
        
        void Update()
        {
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

            actor.getAnimator.Stand();

            if (Input.GetKey(KeyCode.A))
            {
                transform.GetChild(0).eulerAngles = new Vector3(0f, 180f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    actor.getAnimator.Run();
                    velocity += Vector2.left * speed * 2f;
                }                  
                else
                {
                    actor.getAnimator.Walk();
                    velocity += Vector2.left * speed;
                }    
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.GetChild(0).eulerAngles = new Vector3(0f, 0f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    actor.getAnimator.Run();
                    velocity += Vector2.right * speed * 2f;
                }
                else
                {
                    actor.getAnimator.Walk();
                    velocity += Vector2.right * speed;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
                velocity += Vector2.up * 100;  
        }

        void FixedUpdate()
        {
            actor.getPosition.Add(velocity, ForceMode2D.Impulse);
            velocity = Vector3.zero;
        }
    }
}