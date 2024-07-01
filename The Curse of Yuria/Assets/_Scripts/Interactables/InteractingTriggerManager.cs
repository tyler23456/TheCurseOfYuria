using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingTriggerManager : MonoBehaviour
{
    public static InteractingTriggerManager instance { get; set; }

    [SerializeField] Transform allies;

    IInteractableTrigger target;
    IInteractable[] targets;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        target = null;

        if (!GameStateManager.Instance.isPlaying)
            return;
        
        Ray ray = new Ray(allies.GetChild(0).position - Vector3.forward, Vector3.forward);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

        foreach (RaycastHit2D hit in hits)
        {
            target = hit.transform.GetComponent<IInteractableTrigger>();

            if (target != null && target.enabled == true)
                break;
        }

        if (target == null || target.enabled == false)
        {
            MarkerManager.instance.DestroyAllMarkersWith("InteractingTrigger");
            return;
        }

        if (MarkerManager.instance.Count("InteractingTrigger") == 0)
            MarkerManager.instance.AddMarker("InteractingTrigger");

        MarkerManager.instance.SetMarkerMessageAt("InteractingTrigger", target.getAction + target.gameObject.name);
        Collider2D collider = target.gameObject.GetComponent<Collider2D>();
        MarkerManager.instance.SetMarkerWorldPositionAt("InteractingTrigger", collider.bounds.center + Vector3.up * collider.bounds.extents.y);

        if (Input.GetMouseButtonDown(0))
        {
            targets = target.gameObject.GetComponents<IInteractable>();
            foreach (IInteractable target in targets)
                target.Interact(allies.GetChild(0).GetComponent<IActor>());
        }
    }
}
