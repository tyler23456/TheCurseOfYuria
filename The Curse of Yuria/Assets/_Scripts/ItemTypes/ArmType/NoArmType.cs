using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "NoArm", menuName = "ArmType/NoArm")]
    public class NoArmType : ArmTypeBase
    {
        public override float Calculate(IActor user, IActor target, float accumulator)
        {
            return accumulator;
        }

        public override void PlaySoundEffect(AudioSource audiosource)
        {

        }
    }
}