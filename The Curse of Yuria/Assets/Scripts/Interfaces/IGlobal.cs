using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobal
{
    List<IPlayer> getParty { get; }
    Camera getCamera { get; }

    IInventory getHelmets { get; }
    IInventory getEarrings { get; }
    IInventory getGlasses { get; }
    IInventory getMasks { get; }
    IInventory getMeleeWeapons1H { get; }
    IInventory getMeleeWeapons2H { get; }
    IInventory getCapes { get; }
    IInventory getArmor { get; }
    IInventory getShields { get; }
    IInventory getBows { get; }
    IInventory getSupplies { get; }

    public void AddToInventory(HeroEditor.Common.Data.ItemSprite item, HeroEditor.Common.Enums.EquipmentPart part, Color? color);
}
