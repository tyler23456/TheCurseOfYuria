using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    [System.Serializable]
    public class Skillset : ISkillset
    {
        [SerializeField] List<string> skills;

        public int count => skills.Count;

        public string GetSkill(int index)
        {
            return skills[index];
        }

        public void AddSkill(string skill)
        {
            skills.Add(skill);
        }

        public void RemoveSkill(string skill)
        {
            skills.Remove(skill);
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