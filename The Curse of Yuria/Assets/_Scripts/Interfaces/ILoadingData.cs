using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoadingData
{
    static int sceneID { get; set; } = 0;
    static Vector2 destination { get; set; } = Vector2.zero;
    static float eulerAngleZ { get; set; } = 0f;
}
