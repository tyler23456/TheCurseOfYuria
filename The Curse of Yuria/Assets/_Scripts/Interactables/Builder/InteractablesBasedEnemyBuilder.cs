using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class InteractablesBasedEnemyBuilder : MonoBehaviour, IPrefabComponentsBuilder
    {
        RandomDrop randomDrop;

        void IPrefabComponentsBuilder.AddComponentsWithAppropriateValuesTo(GameObject prefab)
        {
            randomDrop = prefab.GetComponent<RandomDrop>();

            if (randomDrop == null)
                randomDrop = prefab.gameObject.AddComponent<RandomDrop>();

            randomDrop.enabled = false;
        }

        bool IPrefabComponentsBuilder.HasComponentsWithAppropriateValuesFor(GameObject prefab)
        {
            randomDrop = prefab.GetComponent<RandomDrop>();
            return randomDrop != null;
        }
    }
}