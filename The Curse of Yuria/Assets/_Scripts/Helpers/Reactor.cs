using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reactor
{
    [SerializeField] Scroll action;
    [SerializeField] TargeterBase.Party party;
    [SerializeField] Scroll reaction;
    [SerializeField] TargeterBase target;

    public Scroll getAction => action;
    public TargeterBase.Party getParty => party;
    public Scroll getReaction => reaction;
    public TargeterBase getTarget => target;

}
