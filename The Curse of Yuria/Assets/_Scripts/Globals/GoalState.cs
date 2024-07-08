using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoalState : ScriptableObject
{
    public enum State { enter, stay, exit }

    public abstract void UpdateState(IController controller);
    public abstract void OnDrawGizmosMethod(IController controller);
    public abstract bool CheckForTransition(IController controller);
}
