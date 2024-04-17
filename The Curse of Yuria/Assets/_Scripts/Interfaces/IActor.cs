using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    GameObject getGameObject { get; }
    IPosition getPosition { get; }
    IRotation getRotation { get; }
    IStats getStats { get; }
    void Equip(HeroEditor.Common.Data.ItemSprite item, HeroEditor.Common.Enums.EquipmentPart part, Color? color);
}
