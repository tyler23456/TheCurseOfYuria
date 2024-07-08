using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HeroEditor.Common.Data;

namespace TCOY.Items
{
    public abstract class SkillBase : Skill
    {
        [Space(5)] [SerializeField] protected SkillInfo skillInfo;
        [Space(5)] [SerializeField] protected StatusEffectsInfo statusEffectsInfo;

        public override string type => "";

        public override IEnumerator Use(IActor user, IActor[] targets) { yield return null; }
        public override IEnumerator Use(IActor target) { yield return null; }
        public override void Equip(IActor user) { }
        public override void Unequip(IActor user) { }

        public ArmType armType => skillInfo.armType;
        public override ElementType elementType => skillInfo.elementType;
        public CalculationType calculationType => skillInfo.calculationType;
        public List<BonusType> bonusTypes => skillInfo.bonusTypes;

        bool CheckForStatusEffectOnHits(IActor user, IActor target, IItem item)
        {
            return statusEffectsInfo.CheckForStatusEffectCounters(user, target, item);
        }

        public void CheckStatusEffects(IActor target)
        {
            statusEffectsInfo.CheckStatusEffects(target);
        }

        public override bool TrueForAnyStatusEffect(Func<StatusEffect, bool> predicate)
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