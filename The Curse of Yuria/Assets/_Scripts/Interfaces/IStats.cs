using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    enum Attribute { MaxHP, HP, MaxMP, MP, Strength, Defense, Magic, Aura, Speed, Luck }

    int GetAttribute(Attribute attribute);
    void OffsetAttribute(IStats.Attribute attribute, int offset);
    void ResetAll();
    void ApplyMagicCost(int cost);
    bool ApplySkillCalculation(int power, IStats user, IItem.Group group, IItem.Type type, IItem.Element element);
    int[] GetAttributes();
}
