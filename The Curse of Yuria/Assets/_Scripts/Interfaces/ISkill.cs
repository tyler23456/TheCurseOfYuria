using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;

public interface ISkill : IItem
{
    ArmTypeBase armType { get; }
    ElementTypeBase elementType { get; }
    CalculationTypeBase calculationType { get; }
    List<BonusTypeBase> bonusTypes { get; }

    bool TrueForAnyStatusEffect(Func<IStatusEffect, bool> predicate);
}
