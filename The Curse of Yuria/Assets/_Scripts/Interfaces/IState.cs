using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public enum State { enter, stay, exit }

    string name { get; }
    void UpdateState(IController controller);
    void OnDrawGizmosMethod(IController controller);
}
