using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class CameraFollow : MonoBehaviour
    {
        new Camera camera;

        void Awake()
        {
            camera = GameObject.Find("/DontDestroyOnLoad/Main Camera").GetComponent<Camera>();
        }

        void OnEnable()
        {
            camera.transform.position = transform.position + new Vector3(0f, 0f, -1f);
        }

        void OnDisable()
        {
            
        }

        void LateUpdate()
        {
            if (camera != null)
                camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + new Vector3(0f, 0f, -1f), 0.3f);
        }
    }
}