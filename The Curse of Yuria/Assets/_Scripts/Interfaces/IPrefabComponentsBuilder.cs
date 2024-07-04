using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrefabComponentsBuilder
{
    void AddComponentsWithAppropriateValuesTo(GameObject prefab);
    bool HasComponentsWithAppropriateValuesFor(GameObject prefab);
}
