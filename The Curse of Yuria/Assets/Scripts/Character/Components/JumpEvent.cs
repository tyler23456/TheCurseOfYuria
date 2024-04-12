using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Character
{
    [System.Serializable]
    public class JumpEvent
    {
        [SerializeField] Vector2 initialJumpVelocity = new Vector2(0f, 24f);
        [SerializeField] Vector2 downwardForce = new Vector2(0f, 9.8f);
        Vector2 jumpVelocity = Vector2.zero;

        public bool isActive { get; private set; } = false;
        public Vector2 getJumpVelocity => jumpVelocity;

        public void Start()
        {
            isActive = true;
            jumpVelocity = initialJumpVelocity;
        }

        public void Update()
        {
            AddJumpVelocity();
        }

        void AddJumpVelocity()
        {
            if (!isActive)
                return;

            jumpVelocity = Clamp(jumpVelocity - downwardForce, new Vector2(0f, -1f), Vector2.one * float.MaxValue);
        }

        Vector3 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(value.x, min.x, max.x), Mathf.Clamp(value.y, min.y, max.y));
        }
    }
}
