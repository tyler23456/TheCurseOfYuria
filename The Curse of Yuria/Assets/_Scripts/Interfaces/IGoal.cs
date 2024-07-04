using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoal : IState
{
    bool CheckForTransition(IController controller);
}
