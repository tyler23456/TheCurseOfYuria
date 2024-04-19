using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    enum Group { None, Melee, Ranged, Magic }
    enum Type { None,  Damage, Recovery}
    enum Element { None, Fire, Ice, Thunder, Light, Dark, Poison, Sleep, Enthrall, Paralyze, Bleed, Burn, Freeze, Shock, Stun }

    bool enabled { get; }
}
