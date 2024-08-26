using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class InteractablesBuilder : MonoBehaviour, IPrefabComponentsBuilder
    {
        ItemBehaviour item;

        void IPrefabComponentsBuilder.AddComponentsWithAppropriateValuesTo(GameObject prefab)
        {
            item = prefab.GetComponent<ItemBehaviour>();

            if (item == null)
                prefab.AddComponent<ItemBehaviour>();
        }

        bool IPrefabComponentsBuilder.HasComponentsWithAppropriateValuesFor(GameObject prefab)
        {
            item = prefab.GetComponent<ItemBehaviour>();
            return item != null;
        }
    }
}