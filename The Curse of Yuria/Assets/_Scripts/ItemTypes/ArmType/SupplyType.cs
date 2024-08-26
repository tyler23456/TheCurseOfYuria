using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Supply", menuName = "ArmType/Supply")]
    public class SupplyType : ArmTypeBase
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