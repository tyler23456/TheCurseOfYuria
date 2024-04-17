using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleManager
{
    void ExecuteCommand(IActor user, IActor target, ICommand command);
}
