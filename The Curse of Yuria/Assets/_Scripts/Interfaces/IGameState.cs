using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    enum State { Playing, Paused, Stopped }

    static State state { get; set; }
}
