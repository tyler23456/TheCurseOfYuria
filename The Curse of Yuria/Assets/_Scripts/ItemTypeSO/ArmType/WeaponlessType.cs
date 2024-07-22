using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weaponless", menuName = "ArmType/Weaponless")]
public class WeaponlessType : StrengthTypeBase
{

    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return base.Calculate(user, target, accumulator);
    }

    public override void PlaySoundEffect(AudioSource audiosource)
    {
        
    }
}
