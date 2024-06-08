using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reactor
{
    [SerializeField] Scroll action;
    [SerializeField] LayerMask mask;
    [SerializeField] Scroll reaction;
    [SerializeField] TargeterBase target;

    public string getItemName => action.name;
    public LayerMask getMask => mask;
    public Scroll getReaction => reaction;
    public TargeterBase getTargeter => target;

    public string PrintInfo()
    {
        string name = "NA";
        if ((mask.value & (1 << 13) & (1 << 14)) != 0)
            name = "Any";
        else if ((mask.value & (1 << 13)) != 0)
            name = "Allie";
        else if ((mask.value & (1 << 14)) != 0)
            name = "Enemy";

        return "if " + action.name + " on " + name + " then " + reaction.name + " on " + target.name;
    }

}
