using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class Move
{
    [SerializeField] ItemSO skill;
    [SerializeField] TargeterBase target;

    public ItemSO getskill => skill;
    public TargeterBase getTargeter => target;
}
