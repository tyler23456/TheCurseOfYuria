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

            if (Global.instance.getCompletedIds.Contains(getID))
                gameObject.SetActive(false);
        }

        public override void Interact(IActor player)
        {
            ItemTypeBase type = Factory.instance.GetItem(name).itemType;

            Global.instance.inventories[type.name].Add(name, 60); //--------------------------------------
            gameObject.SetActive(false);
            Global.instance.getCompletedIds.Add(getID, 1);
        }
    }
}