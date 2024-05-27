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
            if (Global.instance.getCompletedIds.Contains(getID))
                return;

            foreach (ItemBase item in items)
            {
                Global.instance.inventories[item.itemType.name].Add(item.name);
                Global.instance.obtainedItems.Enqueue(item.name);
            }
            Global.instance.ToggleDisplay(Global.Display.ObtainedItems);

            Global.instance.getCompletedIds.Add(getID, 1);
        }
    }
}