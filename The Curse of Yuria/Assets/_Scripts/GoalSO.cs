using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoalSO : ScriptableObject, IGoal
{
    public abstract bool CheckForTransition(IController controller);
    public abstract void UpdateState(IController controller);
    public abstract void OnDrawGizmosMethod(IController controller);
}
