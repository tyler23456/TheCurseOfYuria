using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;
using UnityEditor;

namespace TCOY.AStar
{
    [ExecuteAlways]
    public class Connection : MonoBehaviour, IConnection
    {
        [SerializeField] Waypoint firstWaypoint;
        [SerializeField] Waypoint secondWaypoint;
        [SerializeField] TCOY.ControllerStates.ActionBase action;

        public IWaypoint getFirstWaypoint => firstWaypoint;
        public IWaypoint getSecondWaypoint => secondWaypoint;
        public IAction getAction => action;

        public void ConnectWaypoints(Waypoint firstWaypoint, Waypoint secondWaypoint)
        {
            this.firstWaypoint = firstWaypoint;
            this.secondWaypoint = secondWaypoint;
        }

        public IWaypoint GetOtherWaypoint(IWaypoint thisWaypoint)
        {
            return thisWaypoint.Equals(firstWaypoint) ? secondWaypoint : firstWaypoint;
        }

        void OnDestroy()
        {
            firstWaypoint.Remove(secondWaypoint);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.clear;
            Gizmos.DrawLine(firstWaypoint.transform.position, secondWaypoint.transform.position);
            var p1 = firstWaypoint.transform.position;
            var p2 = secondWaypoint.transform.position;
            var thickness = 3;
            Handles.DrawBezier(p1, p2, p1, p2, Color.yellow, null, thickness);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.clear;
            Gizmos.DrawLine(firstWaypoint.transform.position, secondWaypoint.transform.position);
            var p1 = firstWaypoint.transform.position;
            var p2 = secondWaypoint.transform.position;
            var thickness = 3;
            Handles.DrawBezier(p1, p2, p1, p2, Color.green, null, thickness);
        }
    }
}