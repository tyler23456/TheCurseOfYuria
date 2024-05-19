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
    IItem GetItem(string name);
    bool HasItem(string name);
    IStatusEffect GetStatusEffect(string name);
    GameObject GetAllie(string partyMemberName);
}
