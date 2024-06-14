using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;

public interface ISkill
{
    bool TrueForAnyStatusEffect(Func<IStatusEffect, bool> predicate);
}
