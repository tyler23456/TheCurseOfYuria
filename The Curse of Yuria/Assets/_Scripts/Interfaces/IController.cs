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
    IAction action { get; set; }
    IGoal goal { get; set; }
    IState.State actionState { get; set; }
    IState.State goalState { get; set; }
}
