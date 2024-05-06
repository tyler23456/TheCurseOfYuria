using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TCOY.Canvas
{
    public class PointerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Action onPointerEnter = () => { };
        public Action onPointerExit = () => { };
        public Action onPointerRightClick = () => { };

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
                onPointerRightClick.Invoke();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke();
        }
    }
}
