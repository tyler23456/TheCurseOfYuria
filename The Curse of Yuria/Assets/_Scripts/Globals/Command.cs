using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour
{
    public IActor user { get; private set; }
    public IItem item { get; private set; }
    public IActor[] targets { get; private set; }

    public bool isCounterable { get; set; } = true;
    public bool isInterruptable { get; set; } = true;

    public void Set(IActor user, IItem item, params IActor[] targets)
    {
        this.user = user;
        this.item = item;
        this.targets = targets;
    }
}
