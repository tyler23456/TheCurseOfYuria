using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.StatusEffects
{
    [CreateAssetMenu(fileName = "NewCancellation", menuName = "StatusEffects/Cancellation")]
    public class Cancellation : StatusEffectBase
    {
        [SerializeField] ParticleSystem particleSystem;
        [SerializeField] List<ElementType> elementTypes;

        public override Effect OnHit(IActor user, IActor target, IItem item)
        {
            if (item is not Skill)
                return new Effect(user, target, item);

            Skill skill = (Skill)item;

            if (elementTypes.Any(i => i.name == skill.elementType.name))
            {
                Destroy(Instantiate(particleSystem.gameObject, target.obj.transform), 10f);
                OnRemove(target);
                return new Effect(user, target, item, true, true);
            }

            return new Effect(user, target, item);
        }

        public override void OnRemove(IActor target)
        {
            base.OnRemove(target);
            target.getStatusEffects.Remove(name);
        }
    }
}
