using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneLoader
{
    void Load(int level, Vector2 position, float eulerAngleZ);
    void MoveParty(Vector2 position, float eulerAngleZ);
}
