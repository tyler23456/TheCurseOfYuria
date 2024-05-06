using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

namespace TCOY.DontDestroyOnLoad
{
    public class Equipment : ItemBase, IItem
    {
        public override void Use(IActor user, IActor[] targets)
        {
            if (targets == null || targets.Length == 0)
                return;

            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            global.StartCoroutine(performAnimation(user, targets));
        }

        protected virtual IEnumerator performAnimation(IActor user, IActor[] targets)
        {
            user.getAnimator.Attack();

            yield return new WaitForSeconds(0.5f);

            foreach (IActor target in targets)
                yield return PerformEffect(user, target);  //may need to start a new coroutine for this? 
        }

        protected virtual IEnumerator PerformEffect(IActor user, IActor target)
        {
            target.getStats.ApplySkillCalculation(power, user.getStats, group, type, element);
            Debug.Log("Calculation Applied");
            yield return null;
        }

        public override void Equip(IActor target)
        {
            base.Equip(target);

            IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
            List<string> removedItems = new List<string>();

            switch (category)
            {
                case IItem.Category.meleeWeapons1H:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    factory.GetItem(i).category == IItem.Category.meleeWeapons1H ||
                    factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
                    factory.GetItem(i).category == IItem.Category.bows);
                    target.getAnimator.SetWeaponType(0);
                    break;

                case IItem.Category.meleeWeapons2H:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    factory.GetItem(i).category == IItem.Category.meleeWeapons1H ||
                    factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
                    factory.GetItem(i).category == IItem.Category.shields ||
                    factory.GetItem(i).category == IItem.Category.bows);
                    target.getAnimator.SetWeaponType(1);
                    break;

                case IItem.Category.bows:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    factory.GetItem(i).category == IItem.Category.meleeWeapons1H ||
                    factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
                    factory.GetItem(i).category == IItem.Category.shields ||
                    factory.GetItem(i).category == IItem.Category.bows);
                    target.getAnimator.SetWeaponType(3);
                    break;

                default:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    factory.GetItem(i).category == category);
                    break;
            }

            foreach (string removedItem in removedItems)
                factory.GetItem(removedItem).Unequip(target);

            target.getEquipment.Add(itemName);
            target.getCharacter.Equip(itemSprite, IItem.partConverter[category]);
        }

        public override void Unequip(IActor target)
        {
            base.Unequip(target);

            IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
            List<string> removedItems = new List<string>();

            removedItems = target.getEquipment.RemoveWhere(i =>
                    factory.GetItem(i).category == category);

            target.getEquipment.Remove(itemName);
            target.getCharacter.UnEquip(IItem.partConverter[category]);
        }
    }
}