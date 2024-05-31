using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Container : InteractableBase, IContainer
    {

        [SerializeField] List<Entry> entries;

        public override void Interact(IActor player)
        {
            if (InventoryManager.Instance.completedIds.Contains(getID))
                return;

            foreach (Entry entry in entries)
            {
                InventoryManager.Instance.AddItem(entry.item.name, entry.count);
                ObtainedItemsDisplay.Instance.getInventory.Add(entry.item.name, entry.count);
            }
            ObtainedItemsDisplay.Instance.Refresh();

            InventoryManager.Instance.completedIds.Add(getID, 1);
        }
    }
}