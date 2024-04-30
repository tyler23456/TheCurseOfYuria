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

        [SerializeField] int poison = 0;
        [SerializeField] int sleep = 0;
        [SerializeField] int confuse = 0;
        [SerializeField] int paralyze = 0;
        [SerializeField] int fire = 0;
        [SerializeField] int ice = 0;
        [SerializeField] int thunder = 0;
        [SerializeField] int water = 0;
        [SerializeField] int light = 0;
        [SerializeField] int dark = 0;

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

        public int GetAttribute(IStats.Attributes attribute)
        {
            return attributes[(int)attribute];
        }

        public int GetWeakness(IItem.Element type)
        {
            return weaknesses[(int)type];
        }

        public void OffsetAttribute(IStats.Attributes attribute, int offset)
        {
            attributes[(int)attribute] += offset;
            onStatsChanged.Invoke(attributes);          
        }

        public void ResetAll()
        {
            attributes = new int[] { 0, maxHP, 0, maxMP, 0, strength, defense, aura, speed, luck };
            weaknesses = new int[] { 0, fire, ice, thunder, water, light, dark };
            onStatsChanged.Invoke(attributes);
        }

        public void ApplyMagicCost(int cost)
        {
            attributes[(int)IStats.Attributes.MP] -= cost;
        }

        public bool ApplySkillCalculation(int power, IStats user, IItem.Group group, IItem.Type type, IItem.Element element)
        {
            if (type == IItem.Type.Damage)
                if (group >= IItem.Group.Magic)
                {
                    int defense = GetAttribute(IStats.Attributes.Aura) + GetWeakness(element);
                    int total = (user.GetAttribute(IStats.Attributes.Magic) + power) * (100 / (100 + defense));
                    onApplyDamage.Invoke(total);
                }
                else if (group >= IItem.Group.Melee)
                {
                    int defense = GetAttribute(IStats.Attributes.Defense) + GetWeakness(element);
                    int total = (user.GetAttribute(IStats.Attributes.Strength) + power) * (100 / (100 + defense));
                    onApplyDamage.Invoke(total);
                }
                else if (group >= IItem.Group.None)
                {
                    int defense = GetWeakness(element);
                    int total = power * (100 / (100 + defense));
                    onApplyDamage.Invoke(total);
                }
            else if (type == IItem.Type.Recovery)
                    if (group >= IItem.Group.Magic)
                    {
                        int total = user.GetAttribute(IStats.Attributes.Magic) + power;
                        onApplyDamage.Invoke(total);
                    }
                    else if (group >= IItem.Group.Melee)
                    {
                        int total = user.GetAttribute(IStats.Attributes.Strength) + power;
                        onApplyDamage.Invoke(total);
                    }
                    else if (group >= IItem.Group.None)
                    {
                        int total = power;
                        onApplyDamage.Invoke(total);
                    }

            return false; //this will test whether an effect takes place
        }
    }
}