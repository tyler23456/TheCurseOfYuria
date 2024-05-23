using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;
using Assets.HeroEditor.Common.Scripts.Data;

public interface IFactory
{
    GameObject getDamagePopupPrefab { get; }
    GameObject getRecoveryPopupPrefab { get; }

    HelmetType getHelmet { get; }
    EarringType getEarring { get; }
    GlassesType getGlasses { get; }
    MaskType getMask { get; }
    Melee1HType getMelee1H { get; }
    Melee2HType getMelee2H { get; }
    CapeType getCape { get; }
    ArmorType getArmor { get; }
    ShieldType getShield { get; }
    BowType getBow { get; }
    BasicType getBasic { get; }
    ScrollType getScroll { get; }
    GemType getGem { get; }
    QuestItemType getQuestItem { get; }

    IItem GetItem(string name);
    bool HasItem(string name);
    IStatusEffect GetStatusEffect(string name);
    GameObject GetAllie(string partyMemberName);
}
