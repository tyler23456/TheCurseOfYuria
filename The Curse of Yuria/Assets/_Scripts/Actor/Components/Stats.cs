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
        static IStats.Attributes[] defenseType;

        [SerializeField] int MaxHealthPoints;
        [SerializeField] int MaxMagicPoints;
        [SerializeField] int Strength;
        [SerializeField] int Magic;
        [SerializeField] int Defense;
        [SerializeField] int Aura;
        [SerializeField] int Speed;
        [SerializeField] int Luck;

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

        int[] baseStats;
        int[] addedStats;
        int[] baseVulnerability;
        int[] addedVulnerability;

        public Action<int[]> onStatsChanged { get; set; } = (statsDictionary) => { };
        public Action onZeroHealth { get; set; } = () => { };
        public Action<int> onApplyDamage { get; set; } = (damage) => { };
        public Action<int> onApplyRecovery { get; set; } = (recovery) => { };

        public void Initialize()
        {
            defenseType = new IStats.Attributes[] { 
                IStats.Attributes.None, 
                IStats.Attributes.Defense, 
                IStats.Attributes.Defense, 
                IStats.Attributes.Aura };

            baseStats = new int[] { 
                0, 
                MaxHealthPoints, 
                0, 
                MaxMagicPoints, 
                0, 
                Strength, 
                Defense, 
                Aura, 
                Speed, 
                Luck };
            
            baseVulnerability = new int[] { 
                0, 
                fire, 
                ice, 
                thunder, 
                water, 
                light, 
                dark };

            baseStats = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            baseVulnerability = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        }

        public int GetBaseAttribute(IStats.Attributes attribute)
        {
            return baseStats[(int)attribute];
        }

        public int GetAddedAttribute(IStats.Attributes attribute)
        {
            return addedStats[(int)attribute];
        }

        public int GetAttribute(IStats.Attributes attribute)
        {
            return baseStats[(int)attribute] + addedStats[(int)attribute];
        }

        public int GetBaseVulnerability(IAbility.Type type)
        {
            return baseVulnerability[(int)type];
        }

        public int GetAddedVulnerability(IAbility.Type type)
        {
            return addedVulnerability[(int)type];
        }

        public int GetVulnerability(IAbility.Type type)
        {
            return baseVulnerability[(int)type] + addedVulnerability[(int)type];
        }

        public void OffsetAddedAttribute(IStats.Attributes attribute, int offsetValue)
        {
            addedStats[(int)attribute] += offsetValue;
            onStatsChanged.Invoke(addedStats);          
        }

        public void ResetAll()
        {
            Array.Fill(addedStats, 0);
            Array.Fill(addedStats, 0);
            onStatsChanged.Invoke(addedStats);
        }

        public void ApplyMagicCost(int cost)
        {
            addedStats[(int)IStats.Attributes.MP] -= cost;
        }

        public bool ApplyDamage(int attack, IAbility.Group group, IAbility.Type type)
        {
            int defense = GetAttribute(defenseType[(int)group]) + GetVulnerability(type);
            int damage = attack * (100 / (100 + defense));

            onApplyDamage.Invoke(damage);
            return false; //this will test whether an effect takes place
        }

        public void ApplyRecovery(int recovery)
        {
            onApplyRecovery.Invoke(recovery);
        }
    }
}