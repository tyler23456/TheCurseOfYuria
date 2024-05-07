using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneLoader
{
    void Load(int level, Vector2 newPosition);
    void MoveParty(Vector2 newPosition);
}
