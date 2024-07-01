using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class CameraFollow : MonoBehaviour, IEnabler
    {
        [SerializeField] Transform allies;

        private void Start()
        {
            if (allies.childCount == 0)
                return;

            transform.position = allies.GetChild(0).position + new Vector3(0f, 0f, -1f);
        }

        void LateUpdate()
        {
            if (allies.childCount == 0)
                return;

            transform.position = Vector3.Lerp(transform.position, allies.GetChild(0).position + new Vector3(0f, 0f, -1f), 0.3f);
        }
    }
}