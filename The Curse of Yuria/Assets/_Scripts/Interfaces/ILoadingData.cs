using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ILoadingData
{
    static int sceneID { get; set; } = 0;
    static Vector2 destination { get; set; } = Vector2.zero;
    static float eulerAngleZ { get; set; } = 0f;
    static Action onFinishedLoading { get; set; } = () => { };
}
