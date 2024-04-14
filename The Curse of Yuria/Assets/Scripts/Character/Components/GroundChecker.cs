using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Character
{
    [System.Serializable]
    public class GroundChecker
    {
        [SerializeField] Animator animator;

        public void Initialize()
        {

        }

        public void Update()
        {
            if (!Physics.Raycast(animator.transform.position, Vector2.down, 0.2f))
                return;

            
        }
    }
}