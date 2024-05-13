using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class Move
{
    [SerializeField] Scroll skill;
    [SerializeField] TargeterBase target;

    public Scroll getskill => skill;
    public TargeterBase getTargeter => target;
}
