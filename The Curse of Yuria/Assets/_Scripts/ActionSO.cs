using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionSO : ScriptableObject, IAction
{
    public abstract void UpdateState(IController controller);
    public abstract bool CheckForTransition(IController controller);
    public abstract IState GetSisterState();
    public abstract void OnDrawGizmosMethod(IController controller);
}
