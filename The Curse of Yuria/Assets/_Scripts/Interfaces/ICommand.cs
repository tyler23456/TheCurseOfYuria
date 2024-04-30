using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    IActor user { get; }
    IItem item { get; }
    IActor[] targets { get; }
}
