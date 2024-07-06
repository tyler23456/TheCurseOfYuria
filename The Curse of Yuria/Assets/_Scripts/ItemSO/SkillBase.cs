using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.Items
{
    public abstract class SkillBase : ItemBase
    {
        [Space(5)] [SerializeField] protected SkillInfo skillInfo;
        [Space(5)] [SerializeField] protected StatusEffectsInfo statusEffectsInfo;

        public ArmType armType => skillInfo.armType;
        public ElementType elementType => skillInfo.elementType;
        public CalculationType calculationType => skillInfo.calculationType;
        public List<BonusType> bonusTypes => skillInfo.bonusTypes;

        public virtual void Awake()
        {
            skillInfo = new SkillInfo();
            statusEffectsInfo = new StatusEffectsInfo();
        }

        bool CheckForStatusEffectOnHits(IActor user, IActor target, IItem item)
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
    }
}