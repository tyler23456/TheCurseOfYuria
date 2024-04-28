using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;
using Assets.HeroEditor.Common.Scripts.Data;

public interface IFactory
{
    ItemIcon GetIcon(string name);
    Sprite GetItemPrefab(string name);
    GameObject GetParticleSystemPrefab(string name);
    ISkill GetAbilityPrefab(string name);
}
