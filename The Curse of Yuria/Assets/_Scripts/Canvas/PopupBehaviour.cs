using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class PopupBehaviour : MonoBehaviour
    {
        new Camera camera;
        Vector3 startingPosition;

        void Start()
        {
            camera = GameObject.Find("/DontDestroyOnLoad/Main Camera").GetComponent<Camera>();
            startingPosition = transform.position;
            TranslatePosition();
            transform.GetChild(0).gameObject.SetActive(true);
        }

        void Update()
        {
            TranslatePosition();
        }

        void TranslatePosition()
        {
            transform.position = camera.WorldToScreenPoint(startingPosition);
        }
    }
}