using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionSO : ScriptableObject
{
    public string getName => name;

    public abstract void UpdateState(IController controller);
    public abstract bool CheckForTransition(IController controller);
    public abstract IState GetSisterState();
    public abstract void OnDrawGizmosMethod(IController controller);
}
