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
    IConnection pathfindingConnection { get; set; }
    Vector2 contactPoint { get; }
    bool isGrounded { get; }
    bool previousIsGrounded { get; }
    bool isPathfindingPaused { get; set; }
    bool isAutoMovementPaused { get; set; }
    bool forcePathReconnection { get; set; }
}
