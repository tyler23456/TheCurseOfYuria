using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targeter : ScriptableObject
{
    public enum Party { Allie, Enemy, Both }
    public abstract IActor[] CalculateTargets(Vector2 position);
}
