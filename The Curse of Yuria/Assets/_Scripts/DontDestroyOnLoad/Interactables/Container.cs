using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Container : InteractableBase, IContainer
    {
        [SerializeField] List<ItemBase> items;

        public override void Interact(IActor player)
        {
            if (global.getCompletedIds.Contains(getID))
                return;

            foreach (ItemBase item in items)
                global.inventories[item.itemType.name].Add(item.name);

            global.getCompletedIds.Add(getID, 1);
        }
    }
}