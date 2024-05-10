using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier
{
    IStats.Attribute getAttribute { get; }
    int getOffset { get; }
}
