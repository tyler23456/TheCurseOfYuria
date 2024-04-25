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
        

        public void Initialize()
        {
            parts = new string[19] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", };
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        }

        public string GetPart(EquipmentPart part)
        {
            return parts[(int)part];
        }

        public void Equip(EquipmentPart part, string itemName)
        {
            if ((int)part >= 7 && (int)part <= 10)
            {
                parts[7] = "";
                parts[8] = "";
                parts[9] = "";
                parts[10] = "";
                /*character.UnEquip(EquipmentPart.MeleeWeapon1H);
                character.UnEquip(EquipmentPart.MeleeWeapon2H);
                character.UnEquip(EquipmentPart.Bow);*/
            }
            parts[(int)part] = itemName;
            character.Equip(factory.itemSprites[itemName], part);
        }

        public void Unequip(EquipmentPart part)
        {
            character.UnEquip(part);
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