using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    enum Attributes { None, MaxHP, HP, MaxMP, MP, Strength, Defense, Magic, Aura, Speed, Luck }

    int GetBaseAttribute(Attributes attribute);
    int GetAddedAttribute(Attributes attribute);
    int GetAttribute(Attributes attribute);
    void OffsetAddedAttribute(Attributes attribute, int offsetValue);
    void ResetAll();
    void ApplyMagicCost(int cost);
    bool ApplyDamage(int attack, IAbility.Group group, IAbility.Type type);
    void ApplyRecovery(int recovery);
}
