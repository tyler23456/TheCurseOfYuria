using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : Scroll, IItem
{
    public override void Use(IActor user, IActor[] targets)
    {
        IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
        global.inventories[IItem.Category.supplies].Remove(icon.name);
        global.StartCoroutine(performAnimation(user, targets));
    }
}
