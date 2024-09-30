using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerControls
{
    enum State { Normal = 0, Combat = 1, Climb = 2 }
    static State state = State.Normal;
    static bool initializeGoalStatesOnRefresh = true;

    static bool hasPlayerMoved = true;
    static bool isPlayerRunning = true;

    void ResetTargets();
    void Refresh();
    void SetUnselectedDefaultGoal(GoalState goal);
    void SetUnselectedGoal(GoalState goal);
}
