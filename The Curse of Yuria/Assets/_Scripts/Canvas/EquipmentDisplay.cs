using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;
using System.Collections.ObjectModel;

public class EquipmentDisplay : EquipmentDisplayBase
{
    public static EquipmentDisplay Instance { get; protected set; }

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }
}
