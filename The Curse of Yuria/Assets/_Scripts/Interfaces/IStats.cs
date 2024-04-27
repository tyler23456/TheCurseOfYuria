using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    enum Attributes { None, MaxHP, HP, MaxMP, MP, Strength, Defense, Magic, Aura, Speed, Luck }

    int GetAttribute(Attributes attribute);
    void OffsetAttribute(IStats.Attributes attribute, int offset);
    void ResetAll();
    void ApplyMagicCost(int cost);
    bool ApplySkillCalculation(int power, IStats user, ISkill.Group group, ISkill.Type type, ISkill.Element element);
}
