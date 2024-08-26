using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Ranged", menuName = "ArmType/Ranged")]
    public class RangedType : StrengthTypeBase
    {
        public override float Calculate(IActor user, IActor target, float accumulator)
        {
            return base.Calculate(user, target, accumulator);
        }

        public override void PlaySoundEffect(AudioSource audiosource)
        {
            ArmTypeSFXManager.Instance.PlayReleaseBowSFX(audiosource);
        }
    }
}