using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TCOY.Items
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Attacks/Attack")]
    public class Attack : SkillBase, IItem
    {
        public override IEnumerator Use(IActor user, List<IActor> targets)
        {
            string weaponName = user.getEquipment.Find(i =>
        ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon1H ||
        ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon2H ||
        ItemDatabase.Instance.Part(i) == EquipmentPart.Bow);

            if (weaponName == null)
                yield return UseDefaultAttack(user, targets);
            else
                yield return ItemDatabase.Instance.Get(weaponName).Use(user, targets);

            yield return null;
        }

        IEnumerator UseDefaultAttack(IActor user, List<IActor> targets)
        {
            skillInfo.SetDirection(user, targets);

            user.obj.GetComponent<Animator>()?.SetTrigger("Slash");

            foreach (IActor target in targets)
                target.StartCoroutine(skillInfo.PerformAnimation(user, target, this, statusEffectsInfo));

            yield return null;
        }
    }
}