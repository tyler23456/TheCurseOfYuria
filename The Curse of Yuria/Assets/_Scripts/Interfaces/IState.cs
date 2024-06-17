using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void UpdateState(IStateController controller);
    void OnDrawGizmosMethod(IStateController controller);
}
