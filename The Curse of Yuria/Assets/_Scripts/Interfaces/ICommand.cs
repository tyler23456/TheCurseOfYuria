using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    IActor user { get; }
    IItem item { get; }
    List<IActor> targets { get; }
}
