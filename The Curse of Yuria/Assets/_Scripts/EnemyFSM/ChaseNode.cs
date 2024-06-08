using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChaseNode : FSMNode
{
    public override Func<FSMBehaviour, bool> predicate => (fsm) => fsm.animator.GetBool("Ready");
}
