using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;

public interface IFactory
{
    IEnemy GetEnemyPrefab(string enemyPrefabName);
    SpriteCollection getSpriteCollection { get; }
}
