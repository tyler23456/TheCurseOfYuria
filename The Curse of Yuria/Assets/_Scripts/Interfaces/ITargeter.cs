using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
    enum Party { Allie, Enemy, Both }
    IActor[] CalculateTargets(Vector2 position);
}
