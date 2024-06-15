using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class InteractableChain : InteractableBase, IInteractable
    {
        [SerializeField] InteractableBase[] otherInteractables;

        public override void Interact(IActor player)
        {
            base.Interact(player);

            foreach (InteractableBase otherInteractable in otherInteractables)
                otherInteractable.Interact(player);

        }
    }
}