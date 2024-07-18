using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class Container : InteractableBase, IInteractable, IInteractablePointer
    {
        [SerializeField] protected SavedEntry[] entries;

        protected void OnValidate()
        {
            if (entries != null && entries.Length > 0)
            
            entries[0].Initialize("");
            for (int i = 1; i < entries.Length; i++)
                entries[i].Initialize(entries[i - 1].ID);
        }

        public override void Interact(IActor player)
        {
            IObtainedItemsData.inventory.Clear();

            foreach (SavedEntry entry in entries)
            {
                int count = entry.count - InventoryManager.Instance.completedIds.GetCount(entry.ID);

                if (count <= 0)
                    continue;

                IObtainedItemsData.inventory.Add(entry.item.name, count);
            }

            IObtainedItemsData.onClick = OnClick;
            GameObject.Find("/DontDestroyOnLoad/Canvas/ObtainedItemsDisplay").SetActive(true);
        }

        public void OnClick(string itemName)
        {
            foreach (SavedEntry entry in entries)
                if (entry.item.name == itemName)
                    InventoryManager.Instance.completedIds.Add(entry.ID);
        }
    }
}