using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subcommand : MonoBehaviour
{
    public IActor user { get; }
    public IItem item { get; }
    public IActor target { get; }

    public Subcommand(IActor user, IItem item, IActor target)
    {
        this.user = user;
        this.item = item;
        this.target = target;
    }
}
