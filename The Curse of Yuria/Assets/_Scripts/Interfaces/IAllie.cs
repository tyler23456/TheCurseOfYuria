using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAllie : IActor
{
    Rigidbody2D rigidbody2D { get; }
    Animator animator { get; }
}
