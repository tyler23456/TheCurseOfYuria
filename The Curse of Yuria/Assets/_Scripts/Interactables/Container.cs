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
            if (Global.Instance.getCompletedIds.Contains(getID))
                return;

            foreach (Entry entry in entries)
            {
                Global.Instance.inventories[entry.item.itemType.name].Add(entry.item.name, entry.count);
                ObtainedItemsDisplay.Instance.getInventory.Add(entry.item.name, entry.count);
            }
            ObtainedItemsDisplay.Instance.Refresh();

            Global.Instance.getCompletedIds.Add(getID, 1);
        }

        [System.Serializable]
        class Entry
        {
            public ItemBase item;
            public int count;
        }
    }
}