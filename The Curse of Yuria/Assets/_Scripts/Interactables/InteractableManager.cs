using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class InteractableManager : MonoBehaviour
    {
        public static InteractableManager instance { get; set; }

        InteractableBase target;

        private void Awake()
        {
            instance = this;
        }


        void Update()
        {
            target = null;

            if (Global.instance.gameState != Global.GameState.Playing)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            foreach (RaycastHit2D hit in hits)
            {
                target = hit.transform.GetComponent<InteractableBase>();

                if (target != null)
                    break;
            }

            if (target == null)
            {
                SelectionMarkers.instance.DestroyAllMarkers();
                return;
            }

            if (SelectionMarkers.instance.count == 0)
                SelectionMarkers.instance.AddMarker();

            SelectionMarkers.instance.SetMarkerMessageAt(0, target.getAction + target.name);
            Collider2D collider = target.GetComponent<Collider2D>();
            SelectionMarkers.instance.SetMarkerWorldPositionAt(0, collider.bounds.center + Vector3.up * collider.bounds.extents.y);

            if (Input.GetMouseButtonDown(0))
            {
                target.Interact(Global.instance.allies[0]);
            }
        }
    }
}
