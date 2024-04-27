using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

public interface IEquipment
{
    string GetPart(EquipmentPart part);
    void Equip(EquipmentPart part, string itemName);
    void Unequip(EquipmentPart part);
    bool Contains(string name);
    string[] GetSerializedData();
    void SetSerializedData(string[] array);
}
