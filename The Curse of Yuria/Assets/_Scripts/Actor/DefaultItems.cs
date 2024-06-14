using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Enums;

public class DefaultItems : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] List<ItemBase> defaultItems;

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

        foreach (ItemBase item in defaultItems)
            if (item != null && item is IEquipment)
                character.Equip(item.itemSprite, item.itemType.part);
    }

    public ItemBase[] GetDefaultItems()
    {
        return defaultItems.ToArray();
    }
}
