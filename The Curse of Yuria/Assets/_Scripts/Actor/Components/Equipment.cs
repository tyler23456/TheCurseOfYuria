using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Enums;

namespace TCOY.Actors
{
    [System.Serializable]
    public class Equipment : IEquipment
    {
        IFactory factory;
        [SerializeField] Character character;
        string[] parts;

        public void Reset(GameObject obj)
        {
            character = obj.GetComponent<Character>();           
        }

        public void Start(IFactory factory)
        {
            this.factory = factory;
            parts = new string[10] { "", "", "", "", "", "", "", "", "", "" };
        }

        public string GetPart(IItem.Category part)
        {
            return parts[(int)part];
        }

        public void Equip(IItem.Category part, string itemName)
        {
            /*if ((int)part >= 7 && (int)part <= 10)
            {
                parts[7] = "";
                parts[8] = "";
                parts[9] = "";
                parts[10] = "";
                character.UnEquip(EquipmentPart.MeleeWeapon1H);
                character.UnEquip(EquipmentPart.MeleeWeapon2H);
                character.UnEquip(EquipmentPart.Bow);
            }*/
            parts[(int)part] = itemName;
            character.Equip(factory.GetItem(itemName).itemSprite, IEquipment.partConverter[part]);
        }

        public void Unequip(IItem.Category part)
        {
            parts[(int)part] = "";
            character.UnEquip(IEquipment.partConverter[part]);
        }

        public bool Contains(string name)
        {
            foreach (string part in parts)
                if (part == name)
                    return true;

            return false;
        }

        public string[] GetSerializedData()
        {
            return parts;
        }

        public void SetSerializedData(string[] array)
        {
            parts = array;
        }
    }
}