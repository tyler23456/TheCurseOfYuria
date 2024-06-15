using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Container : InteractableBase, IInteractable, IInteractablePointer
    {
        [SerializeField] protected SavedEntry[] entries;

        protected override void OnValidate()
        {
            foreach (SavedEntry entry in entries)
                if (entry != null && entry.ID == "None" || entry.ID == "")
                    entry.ID = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();           
        }

        public override void Interact(IActor player)
        {
            if (InventoryManager.Instance.completedIds.Contains(getID))
                return;

            foreach (SavedEntry entry in entries)
            {
                int count = entry.count - InventoryManager.Instance.completedIds.GetCount(entry.ID);

                if (count <= 0)
                    continue;

                InventoryManager.Instance.AddItem(entry.item.name, count);
                ObtainedItemsDisplay.Instance.getInventory.Add(entry.item.name, count);
            }
            ObtainedItemsDisplay.Instance.onClick = OnClick;
            ObtainedItemsDisplay.Instance.ShowExclusivelyInParent();     
        }

        public void OnClick(string itemName)
        {
            foreach (SavedEntry entry in entries)
                if (entry.item.name == itemName)
                    InventoryManager.Instance.completedIds.Add(entry.ID);
        }

        [System.Serializable]
        public class SavedEntry : Entry
        {
            [HideInInspector] [SerializeField] public string ID = "None";
        }
    }
}