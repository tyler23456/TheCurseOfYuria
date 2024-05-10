using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSleep", menuName = "StatusEffects/Sleep")]
public class Sleep : StatusEffectBase, IStatusAilment
{
    public override void Activate(IActor target, float duration)
    {

    }
}
