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

        Dictionary<string, int> dynamicStats = new Dictionary<string, int>();
        Dictionary<string, int> staticStats = new Dictionary<string, int>();

        public Action<Dictionary<string, int>> onStatsChanged { get; set; } = (statsDictionary) => { };
        public Action onZeroHealth { get; set; } = () => { };

        public void Initialize()
        {
            staticStats.Add("MaxHealthPoints", MaxHealthPoints);
            staticStats.Add("MaxMagicPoints", MaxMagicPoints);
            staticStats.Add("Strength", Strength);
            staticStats.Add("Magic", Magic);
            staticStats.Add("Defense", Defense);
            staticStats.Add("Aura", Aura);
            staticStats.Add("Speed", Speed);
            staticStats.Add("Luck", Luck);

            dynamicStats.Add("MaxHealthPoints", MaxHealthPoints);
            dynamicStats.Add("MaxMagicPoints", MaxMagicPoints);
            dynamicStats.Add("Strength", Strength);
            dynamicStats.Add("Magic", Magic);
            dynamicStats.Add("Defense", Defense);
            dynamicStats.Add("Aura", Aura);
            dynamicStats.Add("Speed", Speed);
            dynamicStats.Add("Luck", Luck);
        }

        public int GetDynamicAttributeValue(string attributeName)
        {
            return dynamicStats[attributeName];
        }

        public int GetStaticAttributeValue(string attributeName)
        {
            return staticStats[attributeName];
        }

        public void OffsetDynamicAttributeValue(string attributeName, int offsetValue)
        {
            dynamicStats[attributeName] += offsetValue;
            onStatsChanged.Invoke(dynamicStats);          
        }

        public void ResetAll()
        {
            dynamicStats["MaxHealthPoints"] = MaxHealthPoints;
            dynamicStats["MaxMagicPoints"] = MaxMagicPoints;
            dynamicStats["Strength"] = Strength;
            dynamicStats["Magic"] = Magic;
            dynamicStats["Defense"] = Defense;
            dynamicStats["Aura"] = Aura;
            dynamicStats["Speed"] = Speed;
            dynamicStats["Luck"] = Luck;
            onStatsChanged.Invoke(dynamicStats);
        }
    }
}