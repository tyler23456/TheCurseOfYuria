using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPath
{
    Vector2 position { get; }
    IPath target { get; set; }
    bool pathSuccess { get; set; }
    List<SimpleWaypoint> waypoints { get; set; }
    int waypointIndex { get; set; }
    Vector2[] subWaypoints { get; set; }
    int subWaypointIndex { get; set; }
    IConnection connection { get; set; }
    Vector2 contactPoint { get; }
    bool isTouchingTargetableConnection { get; }
    bool isPathfindingPaused { get; set; }
    bool isInitialized { get; set; }
    bool isAutoMovementPaused { get; set; }
}
