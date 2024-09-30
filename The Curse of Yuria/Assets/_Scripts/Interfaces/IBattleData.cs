using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleData
{
    enum BattleState { None, NormalBattle, BossBattle }

    public static bool isInBattle => battleState != BattleState.None;
    public static bool isInNormalBattle => battleState == BattleState.NormalBattle;
    public static bool isInBossBattle => battleState == BattleState.BossBattle;

    static BattleState battleState { get; set; } = BattleState.None;
    static bool isGameOver { get; set; } = false;
    static LinkedList<IActor> aTBGuagesFilled { get; private set; } = new LinkedList<IActor>();
    static LinkedList<Command> pendingCommands { get; private set; } = new LinkedList<Command>();
    static LinkedList<Command> successfulCommands { get; private set; } = new LinkedList<Command>();

    public static void SetBattleStateToNone()
    {
        battleState = BattleState.None;
    }

    public static void SetBattleStateToNormalBattle()
    {
        battleState = BattleState.NormalBattle;
    }

    public static void SetBattleStateToBossBattle()
    {
        battleState = BattleState.BossBattle;
    }
}
