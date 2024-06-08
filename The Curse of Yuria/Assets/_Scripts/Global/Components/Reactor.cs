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

    public TypeBase getItem => action;
    public LayerMask getMask => mask;
    public Scroll getReaction => reaction;
    public TargeterBase getTargeter => target;

}
