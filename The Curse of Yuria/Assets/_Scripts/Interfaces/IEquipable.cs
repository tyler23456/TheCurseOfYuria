using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    void Equip(IActor target);
    void Unequip(IActor target);
}
