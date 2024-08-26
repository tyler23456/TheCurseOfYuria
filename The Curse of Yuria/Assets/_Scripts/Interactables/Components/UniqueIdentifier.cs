using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    [System.Serializable]
    public class UniqueIdentifier
    {
        [SerializeField] string ID = "";

        public string getID => ID;

        public void Initialize(string prefabIdentifier)
        {
            if (ID == null || ID == "" || ID == prefabIdentifier)
                ID = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();
        }

        public bool IsNotFoundInInventory()
        {
            return !InventoryManager.Instance.completedIds.Contains(ID);
        }

        public void AddToInventory()
        {
            InventoryManager.Instance.completedIds.Add(ID);
        }
    }
}
