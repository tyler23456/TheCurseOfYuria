using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

public interface IEquipment
{
    public string GetPart(EquipmentPart part);
    public void Equip(EquipmentPart part, string itemName);
    public void Unequip(EquipmentPart part);
    public string[] GetSerializedData();
    public void SetSerializedData(string[] array);
}
