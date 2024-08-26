using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Melee", menuName = "ArmType/Melee")]
    public class MeleeType : StrengthTypeBase
    {

        public override float Calculate(IActor user, IActor target, float accumulator)
        {
            return base.Calculate(user, target, accumulator);
        }

        public override void PlaySoundEffect(AudioSource audiosource)
        {
            ArmTypeSFXManager.Instance.PlaySwingSFX(audiosource);
        }
    }
}