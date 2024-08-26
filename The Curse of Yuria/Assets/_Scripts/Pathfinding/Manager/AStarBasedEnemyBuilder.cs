using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Pathfinding
{
    public class AStarBasedEnemyBuilder : MonoBehaviour, IPrefabComponentsBuilder
    {
        [SerializeField] GoalState initialGoalState;
        [SerializeField] ActionState initialActionState;

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