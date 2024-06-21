using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnection
{
    IWaypoint getFirstWaypoint { get; }
    IWaypoint getSecondWaypoint { get; }
    IAction getAction { get; }
    IWaypoint GetOtherWaypoint(IWaypoint thisWaypoint);
}
