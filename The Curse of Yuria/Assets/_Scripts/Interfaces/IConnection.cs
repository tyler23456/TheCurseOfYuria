using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnection
{
    IWaypoint getNextWaypoint { get; }
    IAction getAction { get; }
}
