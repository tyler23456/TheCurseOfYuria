using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : ICharacter
{
    IEquipped getEquipped { get; }
}
