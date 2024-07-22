using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CalculationTypeBase : CalculationType
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return 0f;
    }

    public override void PlaySoundEffect(AudioSource audiosource)
    {
    }
}
