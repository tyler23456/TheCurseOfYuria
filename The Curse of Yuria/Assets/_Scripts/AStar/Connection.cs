using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;

namespace TCOY.AStar
{
    [System.Serializable]
    public class Connection : IConnection
    {
        [SerializeField] public Waypoint waypoint;
        [SerializeField] public TCOY.ControllerStates.ActionBase actionState;

        public IWaypoint getNextWaypoint => waypoint;
        public IAction getAction => actionState;
    }
}