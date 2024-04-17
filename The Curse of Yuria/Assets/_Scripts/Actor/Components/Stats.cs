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
        [SerializeField] int MaxHealthPoints;
        [SerializeField] int MaxMagicPoints;
        [SerializeField] int Strength;
        [SerializeField] int Magic;
        [SerializeField] int Defense;
        [SerializeField] int Aura;
        [SerializeField] int Speed;
        [SerializeField] int Luck;

        [SerializeField] int poisonVulnerability;
        [SerializeField] int sleepVulnerability;
        [SerializeField] int confuseVulnerability;
        [SerializeField] int paralyzeVulnerability;
        [SerializeField] int fireVulnerability;
        [SerializeField] int iceVulnerability;
        [SerializeField] int thunderVulnerability;
        [SerializeField] int waterVulnerability;
        [SerializeField] int lightVulnerability;
        [SerializeField] int darkVulnerability;

        Dictionary<string, int> baseStats = new Dictionary<string, int>();
        Dictionary<string, int> addedStats = new Dictionary<string, int>();
        Dictionary<string, int> baseVulnerability = new Dictionary<string, int>();
        Dictionary<string, int> addedVulnerability = new Dictionary<string, int>();

        public Action<Dictionary<string, int>> onStatsChanged { get; set; } = (statsDictionary) => { };
        public Action onZeroHealth { get; set; } = () => { };
        public Action<int> onApplyDamage { get; set; } = (damage) => { };

        public void Initialize()
        {
            baseStats.Add("MaxHealthPoints", MaxHealthPoints);
            baseStats.Add("MaxMagicPoints", MaxMagicPoints);
            baseStats.Add("Strength", Strength);
            baseStats.Add("Magic", Magic);
            baseStats.Add("Defense", Defense);
            baseStats.Add("Aura", Aura);
            baseStats.Add("Speed", Speed);
            baseStats.Add("Luck", Luck);

            baseVulnerability.Add("None", Luck);
            baseVulnerability.Add("Poison", MaxHealthPoints);
            baseVulnerability.Add("Sleep", MaxMagicPoints);
            baseVulnerability.Add("Confuse", Strength);
            baseVulnerability.Add("Paralyze", Magic);
            baseVulnerability.Add("Fire", Defense);
            baseVulnerability.Add("Ice", Aura);
            baseVulnerability.Add("Thunder", Speed);
            baseVulnerability.Add("Water", Luck);
            baseVulnerability.Add("Light", Luck);
            baseVulnerability.Add("Dark", Luck);

            addedStats.Add("MaxHealthPoints", 0);
            addedStats.Add("HealthPoints", 0);
            addedStats.Add("MaxMagicPoints", 0);
            addedStats.Add("MagicPoints", 0);
            addedStats.Add("Strength", 0);
            addedStats.Add("Magic", 0);
            addedStats.Add("Defense", 0);
            addedStats.Add("Aura", 0);
            addedStats.Add("Speed", 0);
            addedStats.Add("Luck", 0);

            addedVulnerability.Add("None", 0);
            addedVulnerability.Add("Poison", 0);
            addedVulnerability.Add("Sleep", 0);
            addedVulnerability.Add("Confuse", 0);
            addedVulnerability.Add("Paralyze", 0);
            addedVulnerability.Add("Fire", 0);
            addedVulnerability.Add("Ice", 0);
            addedVulnerability.Add("Thunder", 0);
            addedVulnerability.Add("Water", 0);
            addedVulnerability.Add("Light", 0);
            addedVulnerability.Add("Dark", 0);
        }

        public int GetBaseAttributeValue(string attributeName)
        {
            return baseStats[attributeName];
        }

        public int GetAddedAttributeValue(string attributeName)
        {
            return addedStats[attributeName];
        }

        public int GetTotalAttributeValue(string attributeName)
        {
            return baseStats[attributeName] + addedStats[attributeName];
        }

        public int GetBaseVulnerabilityValue(string typeName)
        {
            return baseVulnerability[typeName];
        }

        public int GetAddedVulnerabilityValue(string typeName)
        {
            return addedVulnerability[typeName];
        }

        public int GetTotalVulnerabilityValue(string typeName)
        {
            return baseVulnerability[typeName] + addedVulnerability[typeName];
        }

        public void OffsetAddedAttributeValue(string attributeName, int offsetValue)
        {
            addedStats[attributeName] += offsetValue;
            onStatsChanged.Invoke(addedStats);          
        }

        public void ResetAll()
        {
            addedStats.Add("MaxHealthPoints", 0);
            addedStats.Add("MaxMagicPoints", 0);
            addedStats.Add("Strength", 0);
            addedStats.Add("Magic", 0);
            addedStats.Add("Defense", 0);
            addedStats.Add("Aura", 0);
            addedStats.Add("Speed", 0);
            addedStats.Add("Luck", 0);

            addedVulnerability["None"] = 0;
            addedVulnerability["Poison"] = 0;
            addedVulnerability["Sleep"] = 0;
            addedVulnerability["Confuse"] = 0;
            addedVulnerability["Paralyze"] = 0;
            addedVulnerability["Fire"] = 0;
            addedVulnerability["Ice"] = 0;
            addedVulnerability["Thunder"] = 0;
            addedVulnerability["Water"] = 0;
            addedVulnerability["Light"] = 0;
            addedVulnerability["Dark"] = 0;
            
            onStatsChanged.Invoke(addedStats);
        }

        public void ApplyPhysicalDamage(int attack, string type)
        {
            int defense = baseStats["Defense"] + addedStats["Defense"] + baseStats[type] + addedStats[type];     
            int damage = attack * (100 / (100 + defense));
            onApplyDamage.Invoke(damage);
        }

        public void ApplyMagicalDamage(int attack, string type)
        {
            int defense = baseStats["Aura"] + addedStats["Aura"];
            int damage = attack * (100 / (100 + defense));
            onApplyDamage.Invoke(damage);
        }
    }
}