using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy : IActor
{
    Rigidbody2D rigidbody2D { get; }
    Animator animator { get; }
}
