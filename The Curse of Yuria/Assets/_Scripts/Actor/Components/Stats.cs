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
        [SerializeField] int maxHP;
        [SerializeField] int maxMP;
        [SerializeField] int strength;
        [SerializeField] int magic;
        [SerializeField] int defense;
        [SerializeField] int aura;
        [SerializeField] int speed;
        [SerializeField] int luck;

        [SerializeField] int poison;
        [SerializeField] int sleep;
        [SerializeField] int confuse;
        [SerializeField] int paralyze;
        [SerializeField] int fire;
        [SerializeField] int ice;
        [SerializeField] int thunder;
        [SerializeField] int water;
        [SerializeField] int light;
        [SerializeField] int dark;

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

        public int GetWeakness(IAbility.Element type)
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

        public bool ApplyAbility(int power, IStats user, IAbility.Group group, IAbility.Type type, IAbility.Element element)
        {
            if (type == IAbility.Type.Damage)
                if (group >= IAbility.Group.Magic)
                {
                    int defense = GetAttribute(IStats.Attributes.Aura) + GetWeakness(element);
                    int total = (user.GetAttribute(IStats.Attributes.Magic) + power) * (100 / (100 + defense));
                    onApplyDamage.Invoke(total);
                }
                else if (group >= IAbility.Group.Melee)
                {
                    int defense = GetAttribute(IStats.Attributes.Defense) + GetWeakness(element);
                    int total = (user.GetAttribute(IStats.Attributes.Strength) + power) * (100 / (100 + defense));
                    onApplyDamage.Invoke(total);
                }
                else if (group >= IAbility.Group.None)
                {
                    int defense = GetWeakness(element);
                    int total = power * (100 / (100 + defense));
                    onApplyDamage.Invoke(total);
                }
            else if (type == IAbility.Type.Recovery)
                    if (group >= IAbility.Group.Magic)
                    {
                        int total = user.GetAttribute(IStats.Attributes.Magic) + power;
                        onApplyDamage.Invoke(total);
                    }
                    else if (group >= IAbility.Group.Melee)
                    {
                        int total = user.GetAttribute(IStats.Attributes.Strength) + power;
                        onApplyDamage.Invoke(total);
                    }
                    else if (group >= IAbility.Group.None)
                    {
                        int total = power;
                        onApplyDamage.Invoke(total);
                    }

            return false; //this will test whether an effect takes place
        }
    }
}