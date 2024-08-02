using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPath
{
    Vector2 position { get; }
    Vector2 destination { get; set; }
    bool pathSuccess { get; set; }
    IWaypoint previousWaypoint { get; set; }
    List<IWaypoint> waypoints { get; set; }
    int waypointIndex { get; set; }
    Vector2[] subWaypoints { get; set; }
    int subWaypointIndex { get; set; }
    IConnection connection { get; }
    Vector2 contactPoint { get; }
}
