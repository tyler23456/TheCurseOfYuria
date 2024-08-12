using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CalculationType : TypeSO
{
    public abstract float Calculate(IActor user, IActor target, float accumulator);
    public abstract void PlaySoundEffect(AudioSource audiosource);
}