using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    enum Attributes { None, MaxHP, HP, MaxMP, MP, Strength, Defense, Magic, Aura, Speed, Luck }

    int GetAttribute(Attributes attribute);
    void ResetAll();
    void ApplyMagicCost(int cost);
    bool ApplyAbility(int power, IStats user, IAbility.Group group, IAbility.Type type, IAbility.Element element);
}
