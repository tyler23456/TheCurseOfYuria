using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    [System.Serializable]
    public class Rotation : IRotation
    {
        [SerializeField] Transform transform;

        public bool xAxis = true;
        public bool yAxis = true;

        public float interpolation { get; set; } = 1f;
        Vector2 eulerAngles = Vector2.zero;

        public void Update()
        {
            InterpolateRotation();
        }

        public void SetAxes(bool xAxis, bool yAxis)
        {
            this.xAxis = xAxis;
            this.yAxis = yAxis;
        }

        public void Forward(Vector2 forward)
        {

            this.eulerAngles = Quaternion.LookRotation(forward).eulerAngles;
        }

        public void Add(Vector2 eulerAnglesOffset)
        {
            this.eulerAngles += eulerAnglesOffset;
        }

        public void Forward(Vector2 relativeForward, Vector2 localForward)
        {
            this.eulerAngles = Quaternion.LookRotation(relativeForward).eulerAngles + Quaternion.LookRotation(localForward).eulerAngles;
        }

        void InterpolateRotation()
        {
            eulerAngles.x *= Convert.ToInt32(xAxis);
            eulerAngles.y *= Convert.ToInt32(yAxis);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(eulerAngles), interpolation * 50f * Time.deltaTime);
        }
    }
}