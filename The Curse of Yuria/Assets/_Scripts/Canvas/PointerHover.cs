using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PointerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Action onPointerEnter = () => { };
    public Action onPointerExit = () => { };
    public Action onPointerRightClick = () => { };

    void Start()
    {

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
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

