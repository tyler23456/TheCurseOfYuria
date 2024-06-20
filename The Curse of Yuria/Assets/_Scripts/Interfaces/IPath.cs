using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPath
{
    Vector2 origin { get; }
    Vector2 destination { get; set; }
    bool pathSuccess { get; set; }
    List<IWaypoint> waypoints { get; set; }
    int index { get; set; }
}
