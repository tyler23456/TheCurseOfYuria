using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IObtainedItemsData
{
    static Inventory inventory { get; private set; } = new Inventory();
    static Action<string> onLeftClick { get; set; } = (itemName) => { };
    static Action<string> onRightClick { get; set; } = (itemName) => { };
}
