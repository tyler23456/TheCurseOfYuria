using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Character
{
    public class Position : IPosition
    {
        [SerializeField] bool isActive = false;
        [SerializeField] Rigidbody2D rigidBody2D;

        Vector2 velocity = Vector2.zero;

        public bool GetIsActive()
        {
            return isActive;
        }

        public void SetIsActive(bool isActive)
        {
            this.isActive = isActive;
        }

        public void Set(Vector2 position)
        {
            velocity = -rigidBody2D.velocity + position;
        }

        public void Add(float offsetX)
        {
            velocity += new Vector2(offsetX, 0f);
        }

        public void Update()
        {
            if (!isActive)
                return;

            rigidBody2D.velocity = velocity;
            velocity = Vector2.zero;
        }
    }
}