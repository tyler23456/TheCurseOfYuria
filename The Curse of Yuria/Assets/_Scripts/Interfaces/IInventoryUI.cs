using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInventoryUI
{
    public bool isVertical { get; set; }
    public Vector2Int origin { get; set; }
    public Vector2Int padding { get; set; }
    public Vector2Int windowSize { get; set; }
    public Action<string> OnClick { get; set; }
    public RectTransform buttonParent { get; set; }
    public IInventory inventory { get; set; }

    public void Show();
    public void DisplayVertically();
    public void DisplayHorizonally();
}
