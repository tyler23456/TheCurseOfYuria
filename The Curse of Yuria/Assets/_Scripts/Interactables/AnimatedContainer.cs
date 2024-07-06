using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class AnimatedContainer : Container, IInteractable, IInteractablePointer
    {
        [SerializeField] List<IItem> requiredItems;
        [SerializeField] Prompt onLockedPrompt;

        Animator animator;

        protected override void OnValidate()
        {
            if (requiredItems == null)
                requiredItems = new List<IItem>();

            base.OnValidate();
        }


        protected new void Start()
        {
            base.Start();

            animator = GetComponent<Animator>();

            if (!InventoryManager.Instance.completedIds.Contains(getID) || animator == null)
                return;

            animator.enabled = true;
            animator.Play("Base Layer.Activate", 0, 1f);
        }

        public override void Interact(IActor player)
        {
            if (InventoryManager.Instance.completedIds.Contains(getID))
                return;

            if (!requiredItems.TrueForAll(i => InventoryManager.Instance.questItems.Contains(i.name)))
            {
                ShowLockedPrompt();
                return;
            }

            base.Interact(player);

            if (animator != null)
                animator.enabled = true;
        }

        public void ShowLockedPrompt()
        {
            if (onLockedPrompt == null)
                return;

            ActivateScriptedSequence(onLockedPrompt);
        }
    }
}