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

            if (global.getCompletedIds.Contains(getID))
                gameObject.SetActive(false);
        }

        public override void Interact(IAllie player)
        {
            ItemTypeBase type = factory.GetItem(name).itemType;

            global.inventories[type.name].Add(name, 60); //--------------------------------------
            gameObject.SetActive(false);
            global.getCompletedIds.Add(getID, 1);
        }
    }
}