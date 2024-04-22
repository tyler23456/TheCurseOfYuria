using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEquipment
{
    enum Part { helmet, earring, glasses, weapon, weapon2, cape, armor, shield }
    public void Initialize();
    public string GetPart(IEquipment.Part part);
    public void Equip(IEquipment.Part part, string itemName = "None");
    public string[] GetSerializedData();
    public void SetSerializedData(string[] array);
}
