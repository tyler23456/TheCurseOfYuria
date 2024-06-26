using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public IActor user { get; }
    public IItem item { get; }
    public List<IActor> targets { get; }

    public bool isCancelled { get; set; }
    public bool isCounterable { get; set; }
    public bool isInterruptable { get; set; }

    public Command(IActor user, IItem item, List<IActor> targets, bool isCounterable = true, bool isInterruptable = true)
    {
        this.user = user;
        this.item = item;
        this.targets = targets;
        this.isCounterable = isCounterable;
        this.isInterruptable = isInterruptable;
    }
}
