using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;

namespace TCOY.DontDestroyOnLoad
{
    public class Factory : MonoBehaviour, IFactory
    {
        [SerializeField] SpriteCollection spriteCollection;


        public SpriteCollection getSpriteCollection => spriteCollection;

        private void Awake()
        {
            
        }

        public IEnemy GetEnemyPrefab(string enemyPrefabName)
        {
            return null;
        }
    }
}