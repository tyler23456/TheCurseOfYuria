using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMNode : ScriptableObject
{
    [SerializeField] FSMNode[] transitions;

    public virtual Func<FSMBehaviour, bool> predicate => (enemy) => false;

    protected bool isEnter = true;

    public void UpdateNode(FSMBehaviour fsm)
    {
        if (isEnter)
        {
            OnEnter(fsm);
            isEnter = false;
        }

        OnStay(fsm);

        foreach (FSMNode transition in transitions)
            if (transition.predicate(fsm))
            {
                OnExit(fsm);
                fsm.currentNode = transition;
                isEnter = true;
            }
    }

    protected virtual void OnEnter(FSMBehaviour fsm) { }
    protected virtual void OnStay(FSMBehaviour fsm) { }
    protected virtual void OnExit(FSMBehaviour fsm) { }
    

}
