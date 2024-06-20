using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaypoint
{
    Vector2 position { get; }
    List<IConnection> GetConnections();
}
