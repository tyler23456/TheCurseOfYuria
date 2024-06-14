using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScroll : IItem, ISkill
{
    int getCost { get; }
}
