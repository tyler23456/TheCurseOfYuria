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

        public override void Interact(IPartyMember player)
        {
            IItem.Category type = factory.GetItem(name).category;

            global.inventories[type].Add(name, 60); //--------------------------------------
            gameObject.SetActive(false);
            global.getCompletedIds.Add(getID, 1);
        }

        public override void Use(IActor user, IActor[] targets)
        {
            foreach (IActor target in targets)
                Instantiate(gameObject, target.getGameObject.transform);
        }
    }
}