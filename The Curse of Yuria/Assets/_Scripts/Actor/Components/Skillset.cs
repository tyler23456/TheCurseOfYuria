using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    [System.Serializable]
    public class Skillset : ISkillset
    {
        List<string> skills;

        public int count => skills.Count;

        public string GetSkill(int index)
        {
            return skills[index];
        }

        public string[] GetSerializedData()
        {
            return skills.ToArray();
        }

        public void SetSerializedData(string[] array)
        {
            skills.Clear();
            foreach (string element in array)
                skills.Add(element);
        }
    }
}