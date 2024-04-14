using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Character
{
    [System.Serializable]
    public class Climber : IClimber
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

        public void Add(float offsetY)
        {
            velocity += new Vector2(0f, offsetY);
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