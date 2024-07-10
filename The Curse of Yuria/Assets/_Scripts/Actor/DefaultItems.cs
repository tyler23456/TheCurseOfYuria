using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Enums;

public class DefaultItems : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] List<Equipable> defaultItems;

    protected void OnValidate()
    {
        character = GetComponent<Character>();
        character.UnEquip(EquipmentPart.Helmet);
        character.UnEquip(EquipmentPart.Earrings);
        character.UnEquip(EquipmentPart.Glasses);
        character.UnEquip(EquipmentPart.Mask);
        character.UnEquip(EquipmentPart.MeleeWeapon1H);
        character.UnEquip(EquipmentPart.MeleeWeapon2H);
        character.UnEquip(EquipmentPart.Cape);
        character.UnEquip(EquipmentPart.Armor);
        character.UnEquip(EquipmentPart.Shield);
        character.UnEquip(EquipmentPart.Bow);

        if (defaultItems == null)
            return;

        foreach (Equipable item in defaultItems)
            if (item != null)
                character.Equip(item.itemSprite, item.part);
    }

    public Equipable[] GetDefaultItems()
    {
        return defaultItems.ToArray();
    }
}
