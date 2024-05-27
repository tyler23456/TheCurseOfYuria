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

    public TypeBase getItem => action;
    public TargeterBase.Party getParty => party;
    public Scroll getReaction => reaction;
    public TargeterBase getTargeter => target;

}
