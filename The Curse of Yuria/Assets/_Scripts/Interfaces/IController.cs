using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController : IPath
{
    const float stopDistance = 1.5f;
    const float goDistance = 2.25f;
    float accumulator { get; set; }
    Vector2 origin { get; }
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

    void ResetToDefault();
    void SetGoal(GoalState goal);
    void SetAction(ActionState action);

    void StopAllCoroutines();
    Coroutine StartCoroutine(IEnumerator routine);
}
