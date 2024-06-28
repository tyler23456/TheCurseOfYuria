using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IStats
{
    const float OffenseSensitivity = 10f;
    const float DefenseSensitivity = 20f;
    const float weaknessSensitivity = 20f;
    const float powerMultiplier = 10f;

    enum Attribute { MaxHP, MaxMP, Strength, Defense, Magic, Aura, Speed, Luck }
    enum Elements { Fire, Ice, Thunder, Light, Dark }


    Action<int> onHPChanged { get; set; }
    Action<int> onMPChanged { get; set; }

    int HP { get; set; }
    int MP { get; set; }

    int GetAttribute(Attribute attribute);
    int GetWeakness(int index);
    void OffsetAttribute(IStats.Attribute attribute, int offset);
    void OffsetWeakness(int index, int offset);
    void ResetAll();
    void ApplyCost(int cost);
    void ApplyHPDamage(float amount);
    void ApplyHPRecovery(float amount);
    void ApplyMPRecovery(float amount);
    void ApplyMPDamage(float amount);
    int[] GetAttributes();
}
