using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public IActor user { get; private set; }
    public Skill item { get; private set; }
    public List<IActor> targets { get; private set; }

    public bool isCounterable { get; set; } = true;
    public bool isInterruptable { get; set; } = true;

    public Command(IActor user, Skill item, params IActor[] targets)
    {
        this.user = user;
        this.item = item;
        this.targets = new List<IActor>(targets);
    }
}
