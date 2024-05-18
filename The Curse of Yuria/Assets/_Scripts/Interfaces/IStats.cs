using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    enum Attribute { MaxHP, MaxMP, Strength, Defense, Magic, Aura, Speed, Luck }

    int HP { get; set; }
    int MP { get; set; }

    int GetAttribute(Attribute attribute);
    void OffsetAttribute(IStats.Attribute attribute, int offset);
    void SetAttribute(IStats.Attribute attribute, int value);
    void ResetAll();
    void ApplySkillCost(int cost);
    void ApplyCalculation(int power, IItem.Element element);
    bool ApplyCalculation(int power, IStats user, IItem.Group group, IItem.Type type, IItem.Element element);
    int[] GetAttributes();
}
