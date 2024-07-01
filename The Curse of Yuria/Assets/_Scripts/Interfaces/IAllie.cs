using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAllie : IActor
{
    const int MaxActiveAlliesCount = 3;

    Rigidbody2D rigidbody2D { get; }
    Animator animator { get; }
}
