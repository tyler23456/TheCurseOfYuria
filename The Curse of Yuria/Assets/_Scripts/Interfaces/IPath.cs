using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPath
{
    Vector2 position { get; }
    Vector2 destination { get; set; }
    bool pathSuccess { get; set; }
    List<Vector2> waypoints { get; set; }
    int index { get; set; }
    IConnection connection { get; }
}
