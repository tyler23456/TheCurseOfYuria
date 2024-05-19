using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class CameraFollow : MonoBehaviour
    {
        new Camera camera;

        void Start()
        {
            camera = GameObject.Find("/DontDestroyOnLoad/Main Camera").GetComponent<Camera>();
        }

        void LateUpdate()
        {
            if (camera != null)
                camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + new Vector3(0f, 0f, -1f), 0.3f);
        }
    }
}