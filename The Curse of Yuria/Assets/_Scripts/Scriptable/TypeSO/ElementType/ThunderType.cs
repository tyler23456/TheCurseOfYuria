using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder", menuName = "ElementType/Thunder")]
public class ThunderType : ElementTypeBase
{
    public override int weaknessIndex { get; protected set; } = 2;
}
