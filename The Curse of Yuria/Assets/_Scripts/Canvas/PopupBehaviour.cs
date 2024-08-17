using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class PopupBehaviour : MonoBehaviour
    {
        Vector3 startingPosition;

        void Start()
        {
            startingPosition = transform.position;
            transform.GetChild(0).gameObject.SetActive(true);
        }

        void Update()
        {
            TranslatePosition();
        }

        void TranslatePosition()
        {
            transform.position = startingPosition;
        }
    }
}