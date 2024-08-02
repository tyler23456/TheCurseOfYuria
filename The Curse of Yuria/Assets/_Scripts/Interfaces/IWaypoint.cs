using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaypoint
{
    static float distanceThreshold = 0.3f;

    static Color defaultColor { get; private set; } = Color.cyan / 1.5f;
    Color color { get; set; }
    Vector2 position { get; }
    List<IWaypoint> getNeighbors { get; }
    List<IConnection> getConnections { get; }
    IConnection FindConnection(IWaypoint otherWaypoint);
}
