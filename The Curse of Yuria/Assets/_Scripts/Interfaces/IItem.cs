using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

public interface IItem
{
    static Dictionary<Category, EquipmentPart> partConverter = new Dictionary<Category, EquipmentPart>()
    {
        { Category.helmets, EquipmentPart.Helmet },
        { Category.earrings, EquipmentPart.Earrings},
        { Category.glasses, EquipmentPart.Glasses },
        { Category.meleeWeapons1H, EquipmentPart.MeleeWeapon1H },
        { Category.meleeWeapons2H, EquipmentPart.MeleeWeapon2H },
        { Category.capes, EquipmentPart.Cape},
        { Category.armor, EquipmentPart.Armor },
        { Category.shields, EquipmentPart.Shield},
        { Category.bows, EquipmentPart.Bow}
    };

    public enum Category
    {
        helmets,
        earrings,
        glasses,
        masks,
        meleeWeapons1H,
        meleeWeapons2H,
        capes,
        armor,
        shields,
        bows,
        scrolls,
        supplies,
        gems,
        questItems,
    }

    enum Group { None, Melee, Ranged, Magic }
    enum Type { None, Damage, Recovery }
    enum Element { None, Fire, Ice, Thunder, Light, Dark, Poison, Sleep, Enthrall, Paralyze, Bleed, Burn, Freeze, Shock, Stun }

    ulong getGuid { get; }
    string itemName { get; }
    Sprite icon { get; }
    GameObject prefab { get; }
    Category category { get; }
    string getInfo { get; }

    Group getGroup { get; }
    Type getType { get; }
    Element getElement { get; }
    int getPower { get; }
    int getCost { get; }
    float getDuration { get; }

    ItemSprite itemSprite { get; }
    public List<Modifier> getModifiers { get; }
    public List<Reactor> getCounters { get; }
    public List<Reactor> getInterrupts { get; }

    void Use(IActor user, IActor[] targets);
    void Equip(IActor target);
    void Unequip(IActor target);
}
