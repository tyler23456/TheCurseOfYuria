using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipped
{
    void Equip(string equipmentPrefabName, string equipmentType);
    void Unequip(string equipmentType);
}
