using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reactor
{
    public TypeSO type;
    public LayerMask mask;
    public Skill reaction;
    public Targeter target;

    public string PrintInfo()
    {
        string name = "NA";
        if ((mask.value & (1 << 13) & (1 << 14)) != 0)
            name = "Any";
        else if ((mask.value & (1 << 13)) != 0)
            name = "Allie";
        else if ((mask.value & (1 << 14)) != 0)
            name = "Enemy";

        return "if " + type.name + " on " + name + " then " + reaction.name + " on " + target.name;
    }
}
