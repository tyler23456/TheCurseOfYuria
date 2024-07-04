using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemType
{
    string name { get; }
    EquipmentPart part { get; }
}
