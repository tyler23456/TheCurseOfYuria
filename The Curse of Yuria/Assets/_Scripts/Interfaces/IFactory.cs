using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;

public interface IFactory
{
    Dictionary<string, ItemSprite> itemSprites { get; }
    Dictionary<string, IInteractable> itemPrefabs { get; }
    Dictionary<string, GameObject> particleSystemPrefabs { get; }
    Dictionary<string, IAbility> abilityPrefabs { get; }
    Dictionary<string, IEquipable> equipmentPrefabs { get; }
    SpriteCollection getSpriteCollection { get; }
    IEnemy GetEnemyPrefab(string enemyPrefabName);
}
