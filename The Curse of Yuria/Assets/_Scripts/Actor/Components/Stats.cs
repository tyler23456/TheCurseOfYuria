using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace TCOY.UserActors
{
    [System.Serializable]
    public class Stats : IStats
    {
        [SerializeField] int maxHP = 100;
        [SerializeField] int maxMP = 50;
        [SerializeField] int strength = 5;
        [SerializeField] int magic = 5;
        [SerializeField] int defense = 5;
        [SerializeField] int aura = 5;
        [SerializeField] int speed = 5;
        [SerializeField] int luck = 5;

        [SerializeField] int fire = 0;
        [SerializeField] int ice = 0;
        [SerializeField] int thunder = 0;
        [SerializeField] int light = 0;
        [SerializeField] int dark = 0;

        int _HP;
        int _MP;

        public int HP
        {
            get { return _HP; }
            set { _HP = Mathf.Clamp(value, 0, maxHP); }
        }

        public int MP
        {
            get { return _MP; }
            set { _MP = Mathf.Clamp(value, 0, maxMP); }
        }

        int[] attributes;
        int[] weaknesses;

        public Action<int[]> onStatsChanged { get; set; } = (statsDictionary) => { };
        public Action onZeroHealth { get; set; } = () => { };
        public Action<int> onApplyDamage { get; set; } = (damage) => { };
        public Action<int> onApplyRecovery { get; set; } = (recovery) => { };

        public void Initialize()
        {
            ResetAll();
        }

        public int GetAttribute(IStats.Attribute attribute)
        {
            return attributes[(int)attribute];
        }

        public int GetWeakness(int index)
        {
            return weaknesses[index];
        }

        public void OffsetAttribute(IStats.Attribute attribute, int offset)
        {
            attributes[(int)attribute] += offset;
            onStatsChanged.Invoke(attributes);
        }

        public void OffsetWeakness(int index, int offset)
        {
            weaknesses[index] += offset;
            onStatsChanged.Invoke(attributes);
        }

        public void ResetAll()
        {
            HP = maxHP;
            MP = maxMP;
            attributes = new int[8] { maxHP, maxMP, strength, defense, magic, aura, speed, luck };
            weaknesses = new int[5] { fire, ice, thunder, light, dark };
            onStatsChanged.Invoke(attributes);
        }

        public void ApplyCost(int cost)
        {
            MP -= cost;
        }

        public void ApplyDamage(float amount)
        {
            int result = (int)(amount * UnityEngine.Random.Range(0.8f, 1.2f));
            HP -= result;
            onApplyDamage.Invoke(result);
            CheckForZeroHealth();
        }

        public void ApplyRecovery(float amount)
        {
            int result = (int)(amount * UnityEngine.Random.Range(0.8f, 1.2f));
            HP += result;
            onApplyRecovery.Invoke(result);
            CheckForZeroHealth();
        }

        public void CheckForZeroHealth()
        {
            if (HP < 1)
                onZeroHealth.Invoke();
        }

        public int[] GetAttributes()
        {
            return attributes.ToArray();
        }
    }
}