using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targeter : ScriptableObject
{

    public const float DefaultTargetCheckDistance = 15f;
    public enum Party { Allie, Enemy, Both }
    public abstract IActor[] CalculateTargets(Vector2 position, float targetCheckDistance = DefaultTargetCheckDistance);
}
