using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace TCOY.Actors
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

        public int GetWeakness(IItem.Element type)
        {
            return weaknesses[(int)type];
        }

        public void OffsetAttribute(IStats.Attribute attribute, int offset)
        {
            attributes[(int)attribute] += offset;
            onStatsChanged.Invoke(attributes);
        }

        public void SetAttribute(IStats.Attribute attribute, int value)
        {
            attributes[(int)attribute] += value;
            onStatsChanged.Invoke(attributes);
        }

        public void ResetAll()
        {
            HP = maxHP;
            MP = maxMP;
            attributes = new int[8] { maxHP, maxMP, strength, defense, magic, aura, speed, luck };
            weaknesses = new int[6] { fire, ice, thunder, light, dark, 0 }; //need to show effects eventually
            onStatsChanged.Invoke(attributes);
        }

        public void ApplySkillCost(int cost)
        {
            MP -= cost;
        }

        public void ApplyCalculation(int power, IItem.Element element)
        {
            float defense = GetWeakness(element);
            float total = power * (100f / (100f + defense));
            HP -= (int)total;
            onApplyDamage.Invoke((int)total);
            CheckForZeroHealth();
        }

        public bool ApplySkillCalculation(int power, IStats user, IItem.Group group, IItem.Type type, IItem.Element element)
        {
            if (type == IItem.Type.Damage)
                if (group >= IItem.Group.Magic)
                {
                    float defense = GetAttribute(IStats.Attribute.Aura) + GetWeakness(element);
                    float total = (user.GetAttribute(IStats.Attribute.Magic) + power) * (100f / (100f + defense));
                    total = UnityEngine.Random.Range(total * 0.8f, total * 1.2f);
                    HP -= (int)total;
                    onApplyDamage.Invoke((int)total);
                }
                else if (group >= IItem.Group.Melee)
                {
                    float defense = GetAttribute(IStats.Attribute.Defense) + GetWeakness(element);
                    float total = (user.GetAttribute(IStats.Attribute.Strength) + power) * (100f / (100f + defense));
                    total = UnityEngine.Random.Range(total * 0.8f, total * 1.2f);
                    HP -= (int)total;
                    onApplyDamage.Invoke((int)total);
                }
                else if (group >= IItem.Group.None)
                {
                    float defense = GetWeakness(element);
                    float total = power * (100f / (100f + defense));
                    total = UnityEngine.Random.Range(total * 0.8f, total * 1.2f);
                    HP -= (int)total;
                    onApplyDamage.Invoke((int)total);
                }
                else if (type == IItem.Type.Recovery)
                    if (group >= IItem.Group.Magic)
                    {
                        float total = user.GetAttribute(IStats.Attribute.Magic) + power;
                        total = UnityEngine.Random.Range(total * 0.8f, total * 1.2f);
                        HP += (int)total;
                        onApplyRecovery.Invoke((int)total);
                    }
                    else if (group >= IItem.Group.Melee)
                    {
                        float total = user.GetAttribute(IStats.Attribute.Strength) + power;
                        total = UnityEngine.Random.Range(total * 0.8f, total * 1.2f);
                        HP += (int)total;
                        onApplyRecovery.Invoke((int)total);
                    }
                    else if (group >= IItem.Group.None)
                    {
                        float total = power;
                        total = UnityEngine.Random.Range(total * 0.8f, total * 1.2f);
                        HP += (int)total;
                        onApplyRecovery.Invoke((int)total);
                    }

            CheckForZeroHealth();
            return false; //this will test whether an effect takes place
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