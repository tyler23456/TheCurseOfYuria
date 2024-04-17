using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Use(IActor player, IActor[] targets);
}
