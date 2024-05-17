using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class StepSFX
    {
        [SerializeField] Transform transform;

        public void Initialize(GameObject obj)
        {
            transform = obj.GetComponent<Transform>();
        }

        public void Update()
        {

        }
    }
}