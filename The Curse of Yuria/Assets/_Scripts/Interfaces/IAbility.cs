using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    enum Group { None, Melee, Ranged, Magic }
    enum Type { None, Fire, Ice, Thunder, Light, Dark }

    bool enabled { get; }
}
