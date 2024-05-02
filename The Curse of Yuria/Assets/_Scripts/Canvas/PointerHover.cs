using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TCOY.Canvas
{
    public class PointerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Action OnPointerEnter = () => { };
        public Action OnPointerExit = () => { };

        void Start()
        {

        }

        void Update()
        {

        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnter.Invoke();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnPointerExit.Invoke();
        }
    }
}
