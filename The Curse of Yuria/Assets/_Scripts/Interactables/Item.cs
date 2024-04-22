using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Interactables
{
    public class Item : InteractableBase, IInteractable
    {
        
        protected new void Start()
        {
            base.Start();

            if (global.getCompletedIds.Contains(getID))
                gameObject.SetActive(false);
        }

        public override void Interact(IPlayer player)
        {
            global.GetInventoryOf(name).Add(name, 1);
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