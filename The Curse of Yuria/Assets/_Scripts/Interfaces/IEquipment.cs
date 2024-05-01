using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

public interface IEquipment
{
    static Dictionary<IItem.Category, EquipmentPart> partConverter = new Dictionary<IItem.Category, EquipmentPart>()
    {
        { IItem.Category.helmets, EquipmentPart.Helmet },
        { IItem.Category.earrings, EquipmentPart.Earrings},
        { IItem.Category.glasses, EquipmentPart.Glasses },
        { IItem.Category.meleeWeapons1H, EquipmentPart.MeleeWeapon1H },
        { IItem.Category.meleeWeapons2H, EquipmentPart.MeleeWeapon2H },
        { IItem.Category.capes, EquipmentPart.Cape},
        { IItem.Category.armor, EquipmentPart.Armor },
        { IItem.Category.shields, EquipmentPart.Shield},
        { IItem.Category.bows, EquipmentPart.Bow}
    };

    string GetPart(IItem.Category part);
    void Equip(IItem.Category part, string itemName);
    void Unequip(IItem.Category part);
    bool Contains(string name);
    string[] GetSerializedData();
    void SetSerializedData(string[] array);
}
