using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackNode : FSMNode
{
    public override Func<FSMBehaviour, bool> predicate => (fsm) => fsm.animator.GetBool("Ready");

}
