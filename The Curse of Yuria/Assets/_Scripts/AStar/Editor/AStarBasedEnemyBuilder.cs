using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

namespace TCOY.AStar
{
    public class AStarBasedEnemyBuilder : SerializedMonoBehaviour, IPrefabComponentsBuilder
    {
        [OdinSerialize] IGoal initialGoalState;
        [OdinSerialize] IAction initialActionState;

        ControllerUnit unit;

        void IPrefabComponentsBuilder.AddComponentsWithAppropriateValuesTo(GameObject prefab)
        {
            unit = prefab.GetComponent<ControllerUnit>();

            if (unit == null)
                unit = prefab.AddComponent<ControllerUnit>();

            unit = prefab.GetComponent<ControllerUnit>();
            unit.SetInitialStates(initialGoalState, initialActionState);
        }

        bool IPrefabComponentsBuilder.HasComponentsWithAppropriateValuesFor(GameObject prefab)
        {
            unit = prefab.GetComponent<ControllerUnit>();
            return unit != null;
        }
    }
}