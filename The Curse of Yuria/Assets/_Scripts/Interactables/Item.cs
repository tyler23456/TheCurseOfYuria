using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.DontDestroyOnLoad
{
    public class Item : InteractableBase
    {
        protected new void Start()
        {
            base.Start();

            if (InventoryManager.Instance.completedIds.Contains(getID))
                gameObject.SetActive(false);
        }

        public override void Interact(IActor player)
        {
            InventoryManager.Instance.AddItem(name, 60);
            gameObject.SetActive(false);
            InventoryManager.Instance.completedIds.Add(getID, 1);
        }
    }
}