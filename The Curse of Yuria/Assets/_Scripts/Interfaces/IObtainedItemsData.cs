using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IObtainedItemsData
{
    static Inventory inventory { get; private set; } = new Inventory();
    static Action<string> onClick { get; set; } = (itemName) => { };
}
