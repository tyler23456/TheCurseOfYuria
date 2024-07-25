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
    AudioSource audioSource { get; }
    Animator animator { get; }
    Rigidbody2D rigidbody2D { get; }
    ActionState action { get; }
    GoalState goal { get; }
    ActionState.State actionState { get; set; }
    GoalState.State goalState { get; set; }
    void SetGoal(GoalState goal);
    public void SetAction(ActionState action);
}
