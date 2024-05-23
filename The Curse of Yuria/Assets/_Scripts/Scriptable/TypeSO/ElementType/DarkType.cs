using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dark", menuName = "ElementType/Dark")]
public class DarkType : ElementTypeBase
{
    public override int weaknessIndex { get; protected set; } = 4;
}
