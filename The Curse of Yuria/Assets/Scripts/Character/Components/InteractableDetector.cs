using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY
{
    [System.Serializable]
    public class InteractableDetector
    {
        [SerializeField] Transform origin;

        IInteractable interactable = null;

        public void Initialize()
        {

        }

        public void Update()
        {
            if (!Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, 3f))
                return;

            interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable == null)
                return;

            interactable.Interact();
        }
    }
}