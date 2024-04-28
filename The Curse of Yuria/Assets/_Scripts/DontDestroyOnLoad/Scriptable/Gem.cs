using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Gem : ItemBase, IItem
    {
        [SerializeField] int value;
    }
}