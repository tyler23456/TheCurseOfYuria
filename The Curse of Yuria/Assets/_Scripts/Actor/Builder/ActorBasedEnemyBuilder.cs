using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.UserActors
{
    public class ActorBasedEnemyBuilder : MonoBehaviour, IPrefabComponentsBuilder
    {
        Enemy enemy;

        void IPrefabComponentsBuilder.AddComponentsWithAppropriateValuesTo(GameObject prefab)
        {
            enemy = prefab.GetComponent<Enemy>();

            if (enemy == null)
                enemy = prefab.AddComponent<Enemy>();

            Color color = enemy.trajectoryPathColor;

            if (color.r < 0.2f && color.g < 0.2f && color.b < 0.2f)
            {
                float valueA = Random.Range(0.65f, 1f);
                float valueB = Random.Range(0.3f, 1f);
                float valueC = Random.Range(0f, 0.5f);
                List<float> randomValues = new List<float> { valueA, valueB, valueC };
                randomValues = randomValues.OrderBy(i => Random.Range(0, 1)).ToList();
                enemy.trajectoryPathColor = new Color(randomValues[0], randomValues[1], randomValues[2], 0.9f);
            }
        }

        bool IPrefabComponentsBuilder.HasComponentsWithAppropriateValuesFor(GameObject prefab)
        {
            enemy = prefab.GetComponent<Enemy>();
            return enemy != null;
        }
    }
}