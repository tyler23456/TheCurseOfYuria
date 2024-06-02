using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class Destructible : Actor
    {
        Animator userAnimator;
        IContainer[] interactables;

        protected new void Awake()
        {
            base.Awake();

            userAnimator = transform.GetChild(0).GetComponent<Animator>();
            interactables = GetComponents<IContainer>();
            userAnimator.enabled = false;

            stats.onZeroHealth += () => userAnimator.enabled = true;
            stats.onZeroHealth += () =>
            {
                foreach (IContainer interactable in interactables)
                    interactable.Interact(AllieManager.Instance[0]);
            };
        }

        protected new void Update()
        {
            base.Update();
        }
    }
}