using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargeterSO : ScriptableObject
{
    public enum Party { Allie, Enemy, Both }
    public abstract IActor[] CalculateTargets(Vector2 position);
}
