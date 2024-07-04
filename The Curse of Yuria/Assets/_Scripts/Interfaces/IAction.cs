using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction : IState
{
    bool CheckForTransition(IController controller);
    IState GetSisterState();
}