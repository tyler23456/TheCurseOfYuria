using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class Move
{
    [SerializeField] ISkill skill;
    [SerializeField] TargeterBase target;

    public ISkill getskill => skill;
    public TargeterBase getTargeter => target;
}
