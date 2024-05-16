using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.Actors
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        IGlobal global;

        float speed = 1f;
        Actor actor;
        Vector2 velocity = Vector2.zero;
        
        void Start()
        {
            actor = GetComponent<Actor>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
        }
        
        void Update()
        {
            if (Time.timeScale < 0.1f)
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
            {
                //actor.getAnimator.Jump();
                velocity += Vector2.up * 100;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                global.getEquipmentDisplay.gameObject.SetActive(false);
                global.getScrollDisplay.gameObject.SetActive(false);
                global.getCommandDisplay.gameObject.SetActive(false);
                global.getOptionsDisplay.gameObject.SetActive(!global.getOptionsDisplay.gameObject.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                global.getOptionsDisplay.gameObject.SetActive(false);
                global.getScrollDisplay.gameObject.SetActive(false);
                global.getCommandDisplay.gameObject.SetActive(false);
                global.getEquipmentDisplay.gameObject.SetActive(!global.getEquipmentDisplay.gameObject.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                global.getOptionsDisplay.gameObject.SetActive(false);
                global.getEquipmentDisplay.gameObject.SetActive(false);
                global.getCommandDisplay.gameObject.SetActive(false);
                global.getScrollDisplay.gameObject.SetActive(!global.getScrollDisplay.gameObject.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.Tab) && global.aTBGuageFilledQueue.Count > 0)
            {
                global.getEquipmentDisplay.gameObject.SetActive(false);
                global.getScrollDisplay.gameObject.SetActive(false);
                global.getOptionsDisplay.gameObject.SetActive(false);
                global.getCommandDisplay.gameObject.SetActive(!global.getCommandDisplay.gameObject.activeSelf);
            }
        }

        void FixedUpdate()
        {
            actor.getPosition.Add(velocity, ForceMode2D.Impulse);
            velocity = Vector3.zero;
        }
    }
}