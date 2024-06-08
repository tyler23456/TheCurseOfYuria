using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : FSMNode
{
    public override Func<FSMBehaviour, bool> predicate => (fsm) => !fsm.animator.GetBool("Ready");

    protected override void OnEnter(FSMBehaviour fsm)
    {
        
    }

    protected override void OnStay(FSMBehaviour fsm)
    {
        //write code to patrol the area
    }
}
