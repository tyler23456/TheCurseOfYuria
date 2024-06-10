using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerControls
{
    enum State { Normal = 0, Combat = 1, Climb = 2 }
    static State state = State.Normal;
}
