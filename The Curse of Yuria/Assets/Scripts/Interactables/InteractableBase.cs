using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public abstract class InteractableBase : MonoBehaviour
    {
        
        public virtual void Interact(Collider other)
        {
            
        }

        protected void OnTriggerStay(Collider other)
        {
            IPlayerControls playerControls = other.GetComponent<IPlayerControls>();

            if (playerControls == null)
                return;

            Interact(other);
        }
    }
}