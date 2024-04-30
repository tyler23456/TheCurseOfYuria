using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : ICommand
{
    public IActor user { get; }
    public IItem item { get; }
    public IActor[] targets { get; }

    public Command(IActor user, IItem item, IActor[] targets)
    {
        this.user = user;
        this.item = item;
        this.targets = targets;
    }
}
