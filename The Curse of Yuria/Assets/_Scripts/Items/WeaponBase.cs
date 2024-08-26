using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class WeaponBase : EquipableBase, IItem, IEquipment
    {
        [Space(5)] [SerializeField] protected SkillInfo skillInfo;
        [Space(5)] [SerializeField] protected StatusEffectsInfo statusEffectsInfo;

        public ArmType armType => skillInfo.armType;
        public ElementType elementType => skillInfo.elementType;
        public CalculationType calculationType => skillInfo.calculationType;
        public List<BonusType> bonusTypes => skillInfo.bonusTypes;

        public override IEnumerator Use(IActor user, List<IActor> targets)
        {
            skillInfo.SetDirection(user, targets);

            user.obj.GetComponent<Animator>()?.SetTrigger("Slash");

            foreach (IActor target in targets)
                target.StartCoroutine(skillInfo.PerformAnimation(user, target, this, statusEffectsInfo));

            yield return null;
        }

        Effect CheckForStatusEffectOnHits(IActor user, IActor target, IItem item)
        {
            return statusEffectsInfo.CheckForStatusEffectCounters(user, target, item);
        }

        public void CheckStatusEffects(IActor target)
        {
            statusEffectsInfo.CheckStatusEffects(target);
        }

        public bool TrueForAnyStatusEffect(Func<StatusEffect, bool> predicate)
        {
            return statusEffectsInfo.TrueForAnyStatusEffect(predicate);
        }

        public bool ContainsStatusEffectThatCanRemoveKO()
        {
            return statusEffectsInfo.ContainsStatusEffectThatCanRemoveKO();
        }

        public bool IsInvalidTarget(IActor target)
        {
            return statusEffectsInfo.IsInvalidTarget(target);
        }

        public bool ContainsType(string typeName)
        {
            return armType.name == typeName || elementType.name == typeName || calculationType.name == typeName || bonusTypes.Exists(i => i.name == typeName);
        }
    }
}
