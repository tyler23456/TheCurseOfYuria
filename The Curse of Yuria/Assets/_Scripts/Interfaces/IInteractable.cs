using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool enabled { get; }
    void Interact(IPlayer collider);
    void Use(IActor user, IActor[] targets);
}
