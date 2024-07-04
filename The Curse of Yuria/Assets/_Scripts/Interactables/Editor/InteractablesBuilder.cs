using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class InteractablesBuilder : IPrefabComponentsBuilder
    {
        Item item;

        void IPrefabComponentsBuilder.AddComponentsWithAppropriateValuesTo(GameObject prefab)
        {
            item = prefab.GetComponent<Item>();

            if (item == null)
                prefab.AddComponent<Item>();
        }

        bool IPrefabComponentsBuilder.HasComponentsWithAppropriateValuesFor(GameObject prefab)
        {
            item = prefab.GetComponent<Item>();
            return item != null;
        }
    }
}