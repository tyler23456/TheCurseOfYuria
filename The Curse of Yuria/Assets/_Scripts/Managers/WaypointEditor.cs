using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TCOY.AStar
{
    [ExecuteInEditMode]
    public class WaypointEditor : MonoBehaviour
    {
        [SerializeField] Waypoint previousWaypoint;

        Waypoint waypoint;

        private void OnValidate()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;

        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (this == null)
                return;

            List<IWaypoint> waypoints = new List<IWaypoint>();

            foreach (Transform t in transform)
                if (t.TryGetComponent(out IWaypoint waypoint))
                    waypoints.Add(waypoint);

            foreach (IWaypoint waypoint in waypoints)
                if (previousWaypoint == null || previousWaypoint != null && !waypoint.Equals(previousWaypoint))
                    waypoint.color = IWaypoint.defaultColor;


            waypoint = null;

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            foreach (RaycastHit2D hit in hits)
            {
                waypoint = hit.transform.GetComponent<Waypoint>();

                if (waypoint != null)
                    break;
            }

            if (waypoint == null)
                return;

            if (!waypoint.Equals(previousWaypoint))
                waypoint.color = waypoint.color = Color.cyan;

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                LeftMouseClick(waypoint);

            if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
                RightMouseClick(waypoint);
        }

        void LeftMouseClick(Waypoint waypoint)
        {
            if (previousWaypoint == null)
            {
                waypoint.color = Color.green;
                previousWaypoint = waypoint;
            }
            else if (waypoint.Equals(previousWaypoint))
            {
                ResetSelectedWaypoint();
            }
            else
            {               
                AddConnection(waypoint);
                ResetSelectedWaypoint();
            }
        }

        void RightMouseClick(Waypoint waypoint)
        {
            if (previousWaypoint == null)
            {
                waypoint.color = Color.red;
                previousWaypoint = waypoint;
            }
            else if (waypoint.Equals(previousWaypoint))
            {
                ResetSelectedWaypoint();
            }
            else
            {
                RemoveConnection(waypoint);
                ResetSelectedWaypoint();
            }
        }

        void ResetSelectedWaypoint()
        {
            waypoint.color = IWaypoint.defaultColor;
            previousWaypoint = null;
        }

        void AddConnection(Waypoint waypoint)
        {
            if (waypoint.getNeighbors.Contains(previousWaypoint))
                return;

            waypoint.Add(previousWaypoint);
        }

        void RemoveConnection(Waypoint waypoint)
        {
            if (!waypoint.getNeighbors.Contains(previousWaypoint))
                return;

            waypoint.RemoveAndDestroyConnection(previousWaypoint);
        }
    }
}
