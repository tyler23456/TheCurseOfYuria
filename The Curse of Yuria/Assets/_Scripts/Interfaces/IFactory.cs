using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;
using Assets.HeroEditor.Common.Scripts.Data;

public interface IFactory
{
    IItem GetItem(string name);
}
