using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Independent
{
    public class IndependentBasedEnemyBuilder : MonoBehaviour, IPrefabComponentsBuilder
    {
        [SerializeField] Material material;

        MaterialConverter converter;

        void IPrefabComponentsBuilder.AddComponentsWithAppropriateValuesTo(GameObject prefab)
        {
            MaterialConverter converter = prefab.GetComponent<MaterialConverter>();

            if (converter == null)
                converter = prefab.gameObject.AddComponent<MaterialConverter>();

            converter.SetMaterial(material);
            converter.convertToMaterial = true;
        }

        bool IPrefabComponentsBuilder.HasComponentsWithAppropriateValuesFor(GameObject prefab)
        {
            MaterialConverter converter = prefab.GetComponent<MaterialConverter>();
            return converter != null;
        }
    }
}