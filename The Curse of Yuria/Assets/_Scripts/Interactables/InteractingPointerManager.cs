using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class InteractingPointerManager : MonoBehaviour
    {
        public static InteractingPointerManager instance { get; set; }

        IInteractablePointer target;

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
                target = hit.transform.GetComponent<IInteractablePointer>();

                if (target != null && target.enabled == true)
                    break;
            }

            if (target == null || target.enabled == false)
            {
                MarkerManager.instance.DestroyAllMarkersWith("InteractingPointer");
                return;
            }

            if (MarkerManager.instance.Count("InteractingPointer") == 0)
                MarkerManager.instance.AddMarker("InteractingPointer");

            MarkerManager.instance.SetMarkerMessageAt("InteractingPointer", target.getAction + target.gameObject.name);
            Collider2D collider = target.gameObject.GetComponent<Collider2D>();
            MarkerManager.instance.SetMarkerWorldPositionAt("InteractingPointer", collider.bounds.center + Vector3.up * collider.bounds.extents.y);

            if (Input.GetMouseButtonDown(0))
            {
                target.Interact(AllieManager.Instance[0]);
            }
        }
    }
}
