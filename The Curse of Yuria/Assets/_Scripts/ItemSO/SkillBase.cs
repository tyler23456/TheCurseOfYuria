using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.Items
{
    public abstract class SkillBase : ItemBase
    {
        [SerializeField] protected StatusEffectsInfo statusEffectsInfo;
        [SerializeField] protected SkillInfo skillInfo;

        public ArmTypeBase armType => skillInfo.armType;
        public ElementTypeSO elementType => skillInfo.elementType;
        public CalculationTypeBase calculationType => skillInfo.calculationType;
        public List<BonusTypeBase> bonusTypes => skillInfo.bonusTypes;

        bool CheckForStatusEffectCounters(IActor user, IActor target, IItem item)
        {
            return statusEffectsInfo.CheckForStatusEffectCounters(user, target, item);
        }

        public void CheckStatusEffects(IActor target)
        {
            statusEffectsInfo.CheckStatusEffects(target);
        }

        public bool TrueForAnyStatusEffect(Func<IStatusEffect, bool> predicate)
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
    }
}