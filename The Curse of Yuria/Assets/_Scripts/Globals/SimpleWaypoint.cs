using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWaypoint
{
    public Vector2 position { get; }
    public ActionState action { get; }
    public IConnection connection { get; }

    public SimpleWaypoint(Vector2 position, ActionState action, IConnection connection)
    {
        this.position = position;
        this.action = action;
        this.connection = connection;
    }
}
