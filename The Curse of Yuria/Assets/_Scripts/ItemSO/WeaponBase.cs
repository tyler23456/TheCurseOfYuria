using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class WeaponBase : SkillBase, IItem, IEquipment
    {
        [Space(5)] [SerializeField] protected EquipableInfo equipableInfo;

        public virtual EquipmentPart part => EquipmentPart.Armor;

        public ReadOnlyCollection<Modifier> getModifiers => equipableInfo.modifiers.AsReadOnly();
        public ReadOnlyCollection<Ward> getWards => equipableInfo.wards.AsReadOnly();
        public ReadOnlyCollection<Reactor> getCounters => equipableInfo.counters.AsReadOnly();
        public ReadOnlyCollection<Reactor> getInterrupts => equipableInfo.interrupts.AsReadOnly();

        public override void Awake()
        {
            base.Awake();
            equipableInfo = new EquipableInfo();
        }

        public override IEnumerator Use(IActor user, params IActor[] targets)
        {
            skillInfo.SetDirection(user, targets);

            Animator animator = user.obj.GetComponent<Animator>();
            animator?.SetTrigger("Slash");

            foreach (IActor target in targets)
                target.StartCoroutine(skillInfo.PerformAnimation(user, target, this, statusEffectsInfo));

            yield return null;
        }

        public override void Equip(IActor user)
        {
            base.Equip(user);
            equipableInfo.Equip(user, name, part, itemSprite);
        }

        public override void Unequip(IActor user)
        {
            base.Unequip(user);
            equipableInfo.Unequip(user, name, part);
        }
    }
}
