using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWaypoint
{
    public SimpleWaypoint(Vector2 position, ActionState action)
    {
        this.position = position;
        this.action = action;
    }

    public Vector2 position { get; }
    public ActionState action { get; }
}
