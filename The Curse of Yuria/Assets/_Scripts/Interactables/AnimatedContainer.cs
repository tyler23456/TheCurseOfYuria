using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TCOY.Interactables
{
    public class AnimatedContainer : Container, IInteractable, IInteractablePointer
    {
        [SerializeField] UniqueIdentifier uniqueIdentifier;
        [SerializeField] List<Entry> requiredItems;
        [SerializeField] ActionSO onLockedPrompt;

        Animator animator;

        public string getID => uniqueIdentifier.getID;

#if UNITY_EDITOR
        protected new void OnValidate()
        {
            base.OnValidate();

            string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (obj == null)
                return;

            uniqueIdentifier.Initialize(obj.GetComponent<AnimatedContainer>().getID);
        }
#endif

        protected void Start()
        {
            animator = GetComponent<Animator>();

            if (animator == null)
                return;

            if (uniqueIdentifier.IsNotFoundInInventory())
                return;

            animator.enabled = true;
            animator.Play("Base Layer.Activate", 0, 1f);
        }

        public override void Interact(IActor player)
        {
            if (!requiredItems.TrueForAll(i => InventoryManager.Instance.questItems.Contains(i.item.name)))
            {
                ShowLockedPrompt();
                return;
            }

            uniqueIdentifier.AddToInventory();
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