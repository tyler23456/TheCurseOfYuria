using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier
{
    IStats.Attributes getAttribute { get; }
    int getOffset { get; }
}
