using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackFromBehind", menuName = "BonusType/AttackFromBehind")]
public class AttackFromBehindType : BonusTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        Vector3 direction = target.obj.transform.position - user.obj.transform.position;

        if (direction.x > 0)
            accumulator = accumulator * 1.5f;

        return accumulator;
    }
}
