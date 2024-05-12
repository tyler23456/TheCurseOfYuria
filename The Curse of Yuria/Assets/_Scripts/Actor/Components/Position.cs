using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    [System.Serializable]
    public class Position : IPosition
    {
        [SerializeField] Rigidbody2D rigidBody2D;

        int activity = 0;

        public bool isActive => activity == 0;

        public void Set(Vector2 position)
        {
            rigidBody2D.Sleep();
            rigidBody2D.transform.position = position;
            rigidBody2D.WakeUp();
        }

        public void Add(Vector2 offset, ForceMode2D forceMode2D)
        {
            rigidBody2D.AddForce(offset, forceMode2D);
        }

        public void FixedUpdate()
        {
            if (!isActive)
                return;


        }

        public void Update()
        {
            if (!isActive)
                return;


        }

        public void Activate()
        {
            activity++;

            if (activity > 0)
                activity = 0;
        }

        public void Deactivate()
        {
            activity--;
        }
    }
}