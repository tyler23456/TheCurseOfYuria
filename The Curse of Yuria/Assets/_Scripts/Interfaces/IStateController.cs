using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateController
{
    float safeDistance { get; }
    float battleDistance { get; }
    float stopDistance { get; }
    Vector2 velocity { get; set; }
    float speed { get; set; }
    IActor actor { get; }
    Animator animator { get; }
    Rigidbody2D rigidbody2D { get; }
    IState state { get; set; }
}
