using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    Sprite icon { get; set; }
    GameObject prefab { get; set; }
    string getInfo { get; }
}
