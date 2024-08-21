using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransformTeleporter
{
    enum State { arbitrary, Interior, Exterior };
    static State state { get; set; }
}
