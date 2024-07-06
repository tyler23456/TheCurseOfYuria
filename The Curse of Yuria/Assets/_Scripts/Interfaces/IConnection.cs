using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnection
{
    IWaypoint getFirstWaypoint { get; }
    IWaypoint getSecondWaypoint { get; }
    ActionState getAction { get; }
    IWaypoint GetOtherWaypoint(IWaypoint thisWaypoint);
    IWaypoint GetClosestWaypoint(Vector2 position);
}
