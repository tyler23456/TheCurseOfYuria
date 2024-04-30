using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
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
        questItems,
    }
    enum Group { None, Melee, Ranged, Magic }
    enum Type { None, Damage, Recovery }
    enum Element { None, Fire, Ice, Thunder, Light, Dark, Poison, Sleep, Enthrall, Paralyze, Bleed, Burn, Freeze, Shock, Stun }

    int getPower { get; }
    int getCost { get; }
    float getDuration { get; }
    Group getGroup { get; }
    Type getType { get; }


    Element getElement { get; }
    string itemName { get; }
    Sprite icon { get; }
    GameObject prefab { get; }
    string getInfo { get; }
    Category getCategory { get; }

    void Use(IActor user, IActor[] targets);
}
