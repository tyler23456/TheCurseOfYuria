using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public IActor user;
    public IActor target;
    public IItem item;
    public bool itemCancellationFlag = false;
    public bool isSuccessful = false;

    public Effect(IActor user, IActor target, IItem item, bool itemCancellationFlag = false, bool isSuccessful = false)
    {
        this.user = user;
        this.target = target;
        this.item = item;
        this.itemCancellationFlag = itemCancellationFlag;
        this.isSuccessful = isSuccessful;
    }   
}
