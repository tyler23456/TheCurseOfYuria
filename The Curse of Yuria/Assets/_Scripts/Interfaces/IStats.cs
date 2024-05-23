using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    const float Sensitivity = 20f;

    enum Attribute { MaxHP, MaxMP, Strength, Defense, Magic, Aura, Speed, Luck }

    int HP { get; set; }
    int MP { get; set; }

    int GetAttribute(Attribute attribute);
    int GetWeakness(int index);
    void OffsetAttribute(IStats.Attribute attribute, int offset);
    void OffsetWeakness(int index, int offset);
    void ResetAll();
    void ApplyCost(int cost);
    void ApplyDamage(float amount);
    void ApplyRecovery(float amount);
    int[] GetAttributes();
}
