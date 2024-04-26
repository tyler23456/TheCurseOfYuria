using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillset
{
    public int count { get; }
    public string GetSkill(int index);
    void AddSkill(string skill);
    public void RemoveSkill(string skill);
    public string[] GetSerializedData();
    public void SetSerializedData(string[] array);
}
