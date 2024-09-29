using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class CameraFollow : MonoBehaviour, IEnabler
    {
        [SerializeField] Transform allies;
        [SerializeField] Vector3 offset = new Vector3(0f, 0f, -1f);

        Vector3 velocity = Vector3.zero;

        private void Start()
        {
            if (allies.childCount == 0)
                return;

            transform.position = allies.GetChild(0).position + offset;
        }

        void LateUpdate()
        {
            if (allies.childCount == 0)
                return;

            transform.position = Vector3.SmoothDamp(transform.position, allies.GetChild(0).position + offset, ref velocity, 0.1f, float.PositiveInfinity, Time.unscaledDeltaTime);
        }
    }
}