using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : IItem, IEquipment
{
    ArmTypeBase armType { get; }
    ElementTypeBase elementType { get; }
    CalculationTypeBase calculationType { get; }
    List<BonusTypeBase> bonusTypes { get; }
}
