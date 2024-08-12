using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaypoint
{
    static float distanceThreshold = 0.1f;
    static Color defaultColor { get; private set; } = Color.cyan / 1.5f;

    public Vector2 position { get; }
}
