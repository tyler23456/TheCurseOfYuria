using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    IPosition getPosition { get; }
    IRotation getRotation { get; }
    IStats getStats { get; }
}
