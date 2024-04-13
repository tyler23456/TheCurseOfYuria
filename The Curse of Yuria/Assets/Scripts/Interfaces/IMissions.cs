using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissions
{
    void MarkTaskAsCompleted(string missionName, string taskName);
    bool IsTaskCompleted(string missionName, string taskName);
    bool IsMissionCompleted(string missionName);
}
