using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDistanceTargeter", menuName = "Targeters/DistanceTargeter")]
public class DistanceTargeter : TargeterBase
{
    enum Distance { closest, farthest }

    [SerializeField] Distance distance;

    public override List<IActor> GetTargets(Vector2 position)
    {
        base.GetTargets(position);

        float inputValue = 0f;
        float outputValue = distance == Distance.closest? float.PositiveInfinity : float.NegativeInfinity;
        IActor result = null;

        foreach (IActor target in targets)
        {
            inputValue = Vector3.Distance(target.getGameObject.transform.position, position);
            if (distance == Distance.closest && inputValue < outputValue || distance == Distance.farthest && inputValue > outputValue)
            {
                outputValue = inputValue;
                result = target;
            }
        }

        return result == null? null : new List<IActor>() { result };
    }
}