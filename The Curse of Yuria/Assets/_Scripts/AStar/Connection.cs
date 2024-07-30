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
        [SerializeField] [HideInInspector] new EdgeCollider2D collider;

        [SerializeField] Waypoint firstWaypoint;
        [SerializeField] Waypoint secondWaypoint;
        [SerializeField] ActionState action;
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] bool isTrajectory = false;

        public IWaypoint getFirstWaypoint => firstWaypoint;
        public IWaypoint getSecondWaypoint => secondWaypoint;
        public ActionState getAction => action;

        public void OnEnable()
        {
            AddLineRenderer();
            RefreshLineRenderer();
        }

        public void ConnectWaypoints(Waypoint firstWaypoint, Waypoint secondWaypoint)
        {
            this.firstWaypoint = firstWaypoint;
            this.secondWaypoint = secondWaypoint;
            RefreshTransform();
            RefreshCollider();
            RefreshLineRenderer();
        }

        public void AddLineRenderer()
        {
           
        }

        public void RefreshLineRenderer()
        {
            lineRenderer = GetComponent<LineRenderer>();

            if (lineRenderer == null)
                lineRenderer = gameObject.AddComponent<LineRenderer>();

            //need to add vertices to the line renderer
            lineRenderer.positionCount = 3;

            lineRenderer.startWidth = 0.2f;
            lineRenderer.endWidth = 0.2f;
            lineRenderer.SetPosition(0, firstWaypoint.position);
            lineRenderer.SetPosition(2, secondWaypoint.position);
            lineRenderer.sortingOrder = 201;

            if (isTrajectory)
                lineRenderer.SetPosition(1, new Vector2((firstWaypoint.position.x + secondWaypoint.position.x) / 2f, Mathf.Max(firstWaypoint.position.y, secondWaypoint.position.y) + 1.5f));
            else
                lineRenderer.SetPosition(1, (firstWaypoint.position + secondWaypoint.position) / 2);
        }

        public void RefreshTransform()
        {
            Vector2 averagePosition = (firstWaypoint.position + secondWaypoint.position) / 2f;
            transform.position = averagePosition;
        }

        public void Update()
        {
            if (!transform.hasChanged)
                return;

            transform.hasChanged = false;

            RefreshTransformOfAttachedWaypoints();
            RefreshCollider();
            RefreshLineRenderer();
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