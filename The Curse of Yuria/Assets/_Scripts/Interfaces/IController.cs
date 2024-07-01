using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController : IPath
{
    float safeDistance { get; }
    float battleDistance { get; }
    float stopDistance { get; }
    Vector2 velocity { get; set; }
    float speed { get; set; }
    IActor actor { get; }
    Animator animator { get; }
    Rigidbody2D rigidbody2D { get; }
    IAction action { get; }
    IGoal goal { get; }
    IState.State actionState { get; set; }
    IState.State goalState { get; set; }
    void SetGoal(IGoal goal);
    public void SetAction(IAction action);
}
