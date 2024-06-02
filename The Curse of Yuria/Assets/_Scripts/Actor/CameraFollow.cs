using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class CameraFollow : MonoBehaviour
    {
        private void Start()
        {
            if (AllieManager.Instance.count == 0)
                return;

            transform.position = AllieManager.Instance.GetPosition3DAt(0) + new Vector3(0f, 0f, -1f);
        }

        void LateUpdate()
        {
            if (AllieManager.Instance.count == 0)
                return;

            transform.position = Vector3.Lerp(transform.position, AllieManager.Instance.GetPosition3DAt(0) + new Vector3(0f, 0f, -1f), 0.3f);
        }
    }
}