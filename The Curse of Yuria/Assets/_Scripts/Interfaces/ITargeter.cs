using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
    enum Party { Allie, Enemy, Both }
    string name { get; }
    IActor[] CalculateTargets(Vector2 position);
}
