using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    void Equip(IActor target);
    void Unequip(IActor target);
}
