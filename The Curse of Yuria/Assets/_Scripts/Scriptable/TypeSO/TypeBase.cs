using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTypeBase", menuName = "Type/TypeBase")]
public class TypeBase : ScriptableObject
{
    public new string name => base.name;
}
