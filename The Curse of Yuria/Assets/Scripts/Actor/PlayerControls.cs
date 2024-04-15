using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.Actors
{
    public class PlayerControls : MonoBehaviour, IPlayerControls
    {
        [SerializeField] new Transform camera;
        [SerializeField] Animator animator;
        [SerializeField] Rigidbody2D rigidBody;
        [SerializeField] float speed = 5f;
        [SerializeField] float jumpForce = 2f;

        [SerializeField] JumpEvent jumpEvent;

        [SerializeField] Assets.HeroEditor.Common.Scripts.CharacterScripts.Character character;
        [SerializeField] List<Sprite> sprites;

        Vector2 velocity;

        void Start()
        {
            character.Armor.Clear();
            character.Armor.AddRange(sprites);
        }
        
        void Update()
        {
            animator.SetInteger("State", 0);
            Vector2 tempVelocity = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
            {
                transform.GetChild(0).eulerAngles = new Vector3(0f, 180f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetInteger("State", 2);
                    tempVelocity = Vector2.left * speed * 2f;
                }                  
                else
                {
                    animator.SetInteger("State", 1);
                    tempVelocity = Vector2.left * speed;
                }    
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.GetChild(0).eulerAngles = new Vector3(0f, 0f, 0f);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetInteger("State", 2);
                    tempVelocity = Vector2.right * speed * 2f;
                }
                else
                {
                    animator.SetInteger("State", 1);
                    tempVelocity = Vector2.right * speed;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //animator.SetInteger("State", 3);
                jumpEvent.Start();
            }

            jumpEvent.Update();

            velocity = (tempVelocity + jumpEvent.getJumpVelocity);





        }

        public void FixedUpdate()
        {
            rigidBody.velocity = velocity;
            camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + new Vector3(0f, 0f, -1f), 0.3f);
        }
    }
}