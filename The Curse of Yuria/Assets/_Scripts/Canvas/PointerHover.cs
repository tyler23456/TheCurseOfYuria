using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class PointerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image border;

    Color defaultColor;
    
    public Action onPointerEnter = () => { };
    public Action onPointerExit = () => { };
    public Action onPointerRightClick = () => { };

    void Start()
    {
        defaultColor = border.color;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
            onPointerRightClick.Invoke();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter.Invoke();
        border.color = border.color * 3f;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        onPointerExit.Invoke();
        border.color = defaultColor;
    }
}

