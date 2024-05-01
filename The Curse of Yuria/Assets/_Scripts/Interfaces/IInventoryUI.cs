using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInventoryUI
{
    RectTransform grid { get; set; }
    Action<string> OnClick { get; set; }
    IInventory inventory { get; set; }

    void Show();
    void EmptyDisplay();
}
