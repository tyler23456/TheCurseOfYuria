using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRaycastTargeter", menuName = "Targeters/RaycastTargeter")]
public class RaycastTargeter : TargeterBase
{
    [SerializeField] float targetDistanceOverride = DefaultTargetCheckDistance; 

    public override IActor[] CalculateTargets(Vector2 position, float targetDistance = DefaultTargetCheckDistance)
    {
        base.CalculateTargets(position, targetDistanceOverride);

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            Vector2 direction = ((Vector2)targets[i].getCollider2D.bounds.center - position).normalized;

            if (Physics2D.Raycast(position, direction, targetDistanceOverride + 1, LayerMask.GetMask("TileCollision")).collider != null)
                targets.Remove(targets[i]);
        }           

        return targets.ToArray();
    }
}
