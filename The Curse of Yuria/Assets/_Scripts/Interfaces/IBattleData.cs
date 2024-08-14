using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleData
{
    static bool isInBattle { get; set; } = false;
    static LinkedList<IActor> aTBGuagesFilled { get; private set; } = new LinkedList<IActor>();
    static LinkedList<Command> pendingCommands { get; private set; } = new LinkedList<Command>();
    static LinkedList<Command> successfulCommands { get; private set; } = new LinkedList<Command>();

}
