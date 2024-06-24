using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCOY.Heap;
using UnityEditor;

namespace TCOY.AStar
{
    [ExecuteAlways]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Connection : MonoBehaviour, IConnection
    {
        [SerializeField] [HideInInspector] EdgeCollider2D collider;

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
            RefreshTransform();
            RefreshCollider();
        }

        public void RefreshTransform()
        {
            Vector2 averagePosition = (firstWaypoint.position + secondWaypoint.position) / 2;
            transform.position = averagePosition;
        }

        public void Update()
        {
            if (!transform.hasChanged)
                return;

            transform.hasChanged = false;

            RefreshTransformOfAttachedWaypoints();
            RefreshCollider();
        }

        public void RefreshTransformOfAttachedWaypoints()
        {
            firstWaypoint.UpdateTransform();
            secondWaypoint.UpdateTransform();
        }

        public void RefreshCollider()
        {
            if (collider == null)
                collider = gameObject.GetComponent<EdgeCollider2D>();

            collider.isTrigger = true;
            collider.edgeRadius = 0.1f;

            Vector2 halfLength = (secondWaypoint.position - firstWaypoint.position) / 2f;

            collider.points = new Vector2[] { -halfLength, halfLength };
        }


        public IWaypoint GetOtherWaypoint(IWaypoint thisWaypoint)
        {
            return thisWaypoint.Equals(firstWaypoint) ? secondWaypoint : firstWaypoint;
        }

        public IWaypoint GetClosestWaypoint(Vector2 position)
        {
            return Vector2.Distance(position, firstWaypoint.position) > Vector2.Distance(position, secondWaypoint.position)? secondWaypoint : firstWaypoint;
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