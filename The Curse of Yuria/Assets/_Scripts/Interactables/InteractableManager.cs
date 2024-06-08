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

            if (!GameStateManager.Instance.isPlaying)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            foreach (RaycastHit2D hit in hits)
            {
                target = hit.transform.GetComponent<InteractableBase>();

                if (target != null && target.enabled == true)
                     break;
            }

            if (target == null || target.enabled == false)
            {
                MarkerManager.instance.DestroyAllMarkers();
                return;
            }

            if (MarkerManager.instance.count == 0)
                MarkerManager.instance.AddMarker();

            MarkerManager.instance.SetMarkerMessageAt(0, target.getAction + target.name);
            Collider2D collider = target.GetComponent<Collider2D>();
            MarkerManager.instance.SetMarkerWorldPositionAt(0, collider.bounds.center + Vector3.up * collider.bounds.extents.y);

            if (Input.GetMouseButtonDown(0))
            {
                target.Interact(AllieManager.Instance[0]);
            }
        }
    }
}
