using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotation
{
    float lerpSpeed { get; set; }
    void Rotate(Vector2 eulerAnglesOffset);
    void RotateTo(Vector2 eulerAngles);
}
