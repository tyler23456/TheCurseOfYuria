using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.AStar
{
    public class WaypointManager : MonoBehaviour
    {
        [SerializeField] bool AutoGeneratePathsOnReset = false;

        Transform shortestTransform;
        float shortestDistance = float.PositiveInfinity;

        public Transform getShortestTransform => shortestTransform;
        public float getshortestDistance => shortestDistance;

        float calculationTick = 1f;
        float calculationAccumulator = 0f;

        Waypoint currentWaypoint;
        private void Reset()
        {
            foreach (Transform t in transform)
                if (t.GetComponent<Waypoint>() == null)
                    t.gameObject.AddComponent<Waypoint>();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == 0 || i == transform.childCount - 1)
                    continue;

                currentWaypoint = transform.GetChild(i).GetComponent<Waypoint>();
                currentWaypoint.getNeighbors.Clear();
                currentWaypoint.getNeighbors.Add(transform.GetChild(i - 1).GetComponent<Waypoint>());
                currentWaypoint.getNeighbors.Add(transform.GetChild(i + 1).GetComponent<Waypoint>());
            }
        }

        void Start()
        {
            CalculateClosestWaypoint(Global.Instance.allies[0].getGameObject.transform.position);
        }

        void Update()
        {
            if (transform.childCount == 0)
                return;

            calculationAccumulator += Time.deltaTime;

            if (calculationAccumulator < calculationTick)
                return;

            calculationAccumulator = 0f;
            CalculateClosestWaypoint(Global.Instance.allies[0].getGameObject.transform.position);
        }

        public Waypoint CalculateClosestWaypoint(Vector3 position)
        {
            shortestTransform = transform.GetChild(0);
            shortestDistance = float.PositiveInfinity;
            float distance;

            foreach (Transform t in transform)
            {
                distance = Vector3.Distance(position, t.position);

                if (distance < shortestDistance)
                {
                    shortestTransform = t;
                    shortestDistance = distance;
                }
            }

            return shortestTransform.GetComponent<Waypoint>();
        }

        Waypoint waypoint;
        void OnDrawGizmos()
        {
            foreach (Transform t in transform)
            {
                waypoint = t.GetComponent<Waypoint>();

                Gizmos.color = Color.cyan / 1.5f;
                Gizmos.DrawSphere(t.position, 1f);

                if (waypoint == null || waypoint.getNeighbors == null)
                    continue;

                /*Gizmos.color = Color.green;
                foreach (Waypoint w in waypoint.getNeighbors)
                    if (w != null)
                        Gizmos.DrawLine(t.position, w.transform.position);*/
            }
        }
    }
}