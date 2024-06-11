using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IFadeAnimator
{
    Action<IActor> OnCoroutineUpdate { get; set; }
    Action<IActor> OnCoroutineEnd { get; set; }

    void Start(float startColorAlpha, float endingColorAlpha, float duration);
}
