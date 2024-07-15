using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class InteractableWithIDBase : InteractableBase
    {
        [SerializeField] protected string ID = "";

        protected string getID => ID;

        protected void Start()
        {
            if (ID == null || ID == "")
                ID = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();

            if (InventoryManager.Instance.completedIds.Contains(getID))
                gameObject.SetActive(false);
        }
    }
}