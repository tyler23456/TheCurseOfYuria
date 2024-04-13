using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleManager
{
    void ExecuteCommand(ICharacter user, ICharacter target, ICommand command);
}
