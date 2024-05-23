using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemTypeBase : TypeBase
{
    public virtual EquipmentPart part { get; protected set; }

}
