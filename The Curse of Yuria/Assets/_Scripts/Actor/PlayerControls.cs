using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.Actors
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
        }

        void FixedUpdate()
        {
            actor.getPosition.Add(velocity, ForceMode2D.Impulse);
            velocity = Vector3.zero;
        }
    }
}